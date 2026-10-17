using ISoft.Utils.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Composite URI stream provider that handles multiple image sources with caching support
    /// </summary>
    public class RichImageUriStreamProvidersManager : IRichImageUriStreamProvidersManager
    {
        #region Fields
        private readonly ConcurrentDictionary<string, IImageSourceProvider> _providers;
        private readonly ConcurrentDictionary<string, CacheEntry> _streamCache;
        private readonly RichImageUriStreamProvidersOptions _options;
        private IImageSourceProvider _defaultProvider;
        private long _cacheSizeInBytes;
        private bool _disposed;

        // Cache statistics fields
        private long _cacheHits;
        private long _cacheMisses;
        #endregion

        #region Proprties
        public IReadOnlyDictionary<string, IImageSourceProvider> Providers => _providers;

        public bool IsCachingEnabled => _options.EnableCaching;

        public int CacheCount => _streamCache.Count;

        public long CacheSizeInBytes => Interlocked.Read(ref _cacheSizeInBytes);
        #endregion

        public RichImageUriStreamProvidersManager(RichImageUriStreamProvidersOptions options = null)
        {
            _options = options ?? new RichImageUriStreamProvidersOptions();
            _providers = new ConcurrentDictionary<string, IImageSourceProvider>(StringComparer.OrdinalIgnoreCase);
            _streamCache = new ConcurrentDictionary<string, CacheEntry>(StringComparer.OrdinalIgnoreCase);
        }

        public RichImageUriStreamProvidersManager(bool enableCaching, int maxCacheItems = 50, long maxCacheSizeBytes = 50 * 1024 * 1024)
            : this(new RichImageUriStreamProvidersOptions
            {
                EnableCaching = enableCaching,
                MaxCacheItems = maxCacheItems,
                MaxCacheSizeBytes = maxCacheSizeBytes
            })
        {

        }

        #region Implemntation
        /// <summary>
        /// Registers an image source provider
        /// </summary>
        public RichImageUriStreamProvidersManager RegisterProvider(IImageSourceProvider provider)
        {
            ThrowIfDisposed();

            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            if (string.IsNullOrEmpty(provider.Scheme?.Name))
                throw new ArgumentException("Provider scheme name cannot be null or empty", nameof(provider));

            _providers.AddOrUpdate(
                provider.Scheme.Name,
                provider,
                (_, existing) =>
                {
                    if (_options.WarnOnProviderOverride)
                    {
                        Debug.WriteLine($"Provider for scheme '{provider.Scheme.Name}' is being overridden");
                    }
                    return provider;
                });

            return this;
        }

        /// <summary>
        /// Registers multiple providers
        /// </summary>
        public RichImageUriStreamProvidersManager RegisterProviders(params IImageSourceProvider[] providers)
        {
            if (providers == null)
                throw new ArgumentNullException(nameof(providers));

            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

            return this;
        }

        /// <summary>
        /// Unregisters a provider for the specified scheme
        /// </summary>
        public bool UnregisterProvider(string scheme)
        {
            ThrowIfDisposed();
            return _providers.TryRemove(scheme, out _);
        }

        /// <summary>
        /// Sets the default provider for URIs without a scheme
        /// </summary>
        public RichImageUriStreamProvidersManager SetDefaultProvider(IImageSourceProvider provider)
        {
            ThrowIfDisposed();
            _defaultProvider = provider ?? throw new ArgumentNullException(nameof(provider));
            return this;
        }

        /// <summary>
        /// Gets a stream for the specified URI
        /// </summary>
        public Stream GetStream(string uri)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(uri))
                return Stream.Null;

            // Try to get from cache
            if (TryGetFromCache(uri, out var cachedStream))
                return cachedStream;

            try
            {
                var provider = ResolveProvider(uri);
                if (provider == null)
                    return Stream.Null;

                var stream = provider.GetStream(uri);

                if (stream != null && _options.EnableCaching)
                {
                    stream = CacheAndReturnStream(uri, stream);
                }

                return stream ?? Stream.Null;
            }
            catch (Exception ex)
            {
                LogError(ex, uri);
                return Stream.Null;
            }
        }

        /// <summary>
        /// Gets a stream asynchronously with cancellation support
        /// </summary>
        public async Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(uri))
                return Stream.Null;

            cancellationToken.ThrowIfCancellationRequested();

            // Try to get from cache
            if (TryGetFromCache(uri, out var cachedStream))
                return cachedStream;

            try
            {
                var provider = ResolveProvider(uri);
                if (provider == null)
                    return Stream.Null;

                var stream = await provider.GetStreamAsync(uri).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                if (stream != null && _options.EnableCaching)
                {
                    stream = await CacheAndReturnStreamAsync(uri, stream, cancellationToken).ConfigureAwait(false);
                }

                return stream ?? Stream.Null;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex, uri);
                return Stream.Null;
            }
        }

        /// <summary>
        /// Checks if an image exists at the given URI
        /// </summary>
        public bool Exists(string uri)
        {
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(uri))
                return false;

            // Check cache first
            if (_options.EnableCaching && _streamCache.ContainsKey(uri))
                return true;

            var provider = ResolveProvider(uri);
            if (provider != null)
                return provider.Exists(uri);

            return _defaultProvider?.Exists(uri) ?? false;
        }

        /// <summary>
        /// Preloads and caches an image URI
        /// </summary>
        public async Task<bool> PreloadAsync(string uri, CancellationToken cancellationToken = default)
        {
            if (!_options.EnableCaching)
                return false;

            try
            {
                var stream = await GetStreamAsync(uri, cancellationToken).ConfigureAwait(false);
                return stream != Stream.Null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Clears the entire image cache
        /// </summary>
        public void ClearCache()
        {
            _streamCache.Clear();
            Interlocked.Exchange(ref _cacheSizeInBytes, 0);
            OnCacheCleared();
        }

        /// <summary>
        /// Invalidates a specific URI in the cache
        /// </summary>
        public bool InvalidateCache(string uri)
        {
            if (_streamCache.TryRemove(uri, out var entry))
            {
                Interlocked.Add(ref _cacheSizeInBytes, -entry.Size);
                entry.Dispose();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Invalidates all cache entries matching a predicate
        /// </summary>
        public int InvalidateCache(Func<string, bool> predicate)
        {
            var removedCount = 0;
            var keysToRemove = _streamCache.Keys.Where(predicate).ToList();

            foreach (var key in keysToRemove)
            {
                if (InvalidateCache(key))
                    removedCount++;
            }

            return removedCount;
        }

        /// <summary>
        /// Gets cache statistics
        /// </summary>
        public ImageCacheStatistics GetCacheStatistics()
        {
            return new ImageCacheStatistics
            {
                ItemCount = _streamCache.Count,
                TotalSizeBytes = CacheSizeInBytes,
                MaxItems = _options.MaxCacheItems,
                MaxSizeBytes = _options.MaxCacheSizeBytes,
                Hits = Interlocked.Read(ref _cacheHits),
                Misses = Interlocked.Read(ref _cacheMisses)
            };
        }
        #endregion

        #region Privet Actions
        private IImageSourceProvider ResolveProvider(string uri)
        {
            if (!ImageUriScheme.TryParseScheme(uri, out var scheme) || string.IsNullOrEmpty(scheme.Name))
            {
                return _defaultProvider;
            }

            return _providers.TryGetValue(scheme.Name, out var provider) ? provider : _defaultProvider;
        }

        private bool TryGetFromCache(string uri, out Stream stream)
        {
            stream = null;

            if (!_options.EnableCaching)
                return false;

            if (!_streamCache.TryGetValue(uri, out var entry))
            {
                Interlocked.Increment(ref _cacheMisses);
                return false;
            }

            // Check if cache entry has expired
            if (entry.IsExpired(_options.CacheExpirationTime))
            {
                InvalidateCache(uri);
                Interlocked.Increment(ref _cacheMisses);
                return false;
            }

            entry.UpdateLastAccess();
            Interlocked.Increment(ref _cacheHits);
            stream = new MemoryStream(entry.Data, false);
            return true;
        }

        private Stream CacheAndReturnStream(string uri, Stream stream)
        {
            try
            {
                var entry = CacheEntry.FromStream(stream);

                EnsureCacheCapacity(entry.Size);

                if (_streamCache.TryAdd(uri, entry))
                {
                    Interlocked.Add(ref _cacheSizeInBytes, entry.Size);
                }
                else
                {
                    entry.Dispose();
                }

                // Return a fresh stream from the cached data
                if (_streamCache.TryGetValue(uri, out var cachedEntry))
                {
                    return new MemoryStream(cachedEntry.Data, false);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to cache stream for URI: {uri}");
            }

            // If caching fails, return the original stream if possible
            if (stream.CanSeek)
            {
                stream.Position = 0;
                return stream;
            }

            stream.Dispose();
            return null;
        }

        private async Task<Stream> CacheAndReturnStreamAsync(string uri, Stream stream, CancellationToken cancellationToken)
        {
            try
            {
                var entry = await CacheEntry.FromStreamAsync(stream, cancellationToken).ConfigureAwait(false);

                EnsureCacheCapacity(entry.Size);

                if (_streamCache.TryAdd(uri, entry))
                {
                    Interlocked.Add(ref _cacheSizeInBytes, entry.Size);
                }
                else
                {
                    entry.Dispose();
                }

                if (_streamCache.TryGetValue(uri, out var cachedEntry))
                {
                    return new MemoryStream(cachedEntry.Data, false);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to cache stream async for URI: {uri}");
            }

            if (stream.CanSeek)
            {
                stream.Position = 0;
                return stream;
            }

            stream.Dispose();
            return null;
        }

        private void EnsureCacheCapacity(long newEntrySize)
        {
            // Remove expired entries first
            RemoveExpiredEntries();

            // Check item count limit
            while (_streamCache.Count >= _options.MaxCacheItems && _streamCache.Count > 0)
            {
                RemoveLeastRecentlyUsedEntry();
            }

            // Check size limit
            var currentSize = Interlocked.Read(ref _cacheSizeInBytes);
            while (currentSize + newEntrySize > _options.MaxCacheSizeBytes && _streamCache.Count > 0)
            {
                RemoveLeastRecentlyUsedEntry();
                currentSize = Interlocked.Read(ref _cacheSizeInBytes);
            }
        }

        private void RemoveExpiredEntries()
        {
            if (!_options.CacheExpirationTime.HasValue)
                return;

            var expiredKeys = _streamCache
                .Where(kvp => kvp.Value.IsExpired(_options.CacheExpirationTime.Value))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in expiredKeys)
            {
                InvalidateCache(key);
            }
        }

        private void RemoveLeastRecentlyUsedEntry()
        {
            var lruKey = _streamCache
                .OrderBy(kvp => kvp.Value.LastAccessTime)
                .Select(kvp => kvp.Key)
                .FirstOrDefault();

            if (lruKey != null)
            {
                InvalidateCache(lruKey);
            }
        }

        private void LogError(Exception ex, string message)
        {
            if (_options.EnableErrorLogging)
            {
                RunTimeLogger.CurrentLogger.LogError($"[RichImageUriStreamProvidersManager] {message}: {ex.Message}");
            }
        }

        private void OnCacheCleared()
        {
            _options.OnCacheCleared?.Invoke();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(RichImageUriStreamProvidersManager));
        }

        public void Dispose()
        {
            if (_disposed) return;

            // Dispose all cached entries
            foreach (var entry in _streamCache.Values)
            {
                entry.Dispose();
            }

            _streamCache.Clear();
            _providers.Clear();
            _disposed = true;
        }

        #endregion
    }
}
