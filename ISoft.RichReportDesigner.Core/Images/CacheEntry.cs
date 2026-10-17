using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Cache entry with metadata
    /// </summary>
    internal sealed class CacheEntry : IDisposable
    {
        private long _lastAccessTicks;
        private bool _disposed;

        public byte[] Data { get; }

        public long Size { get; }

        public DateTime CreationTime { get; }

        public DateTime LastAccessTime => new(Interlocked.Read(ref _lastAccessTicks), DateTimeKind.Utc);

        private CacheEntry(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Size = data.Length;
            CreationTime = DateTime.UtcNow;
            _lastAccessTicks = CreationTime.Ticks;
        }

        public static CacheEntry FromStream(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return new CacheEntry(memoryStream.ToArray());
        }

        public static async Task<CacheEntry> FromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, 81920, cancellationToken).ConfigureAwait(false);
            return new CacheEntry(memoryStream.ToArray());
        }

        public void UpdateLastAccess()
        {
            Interlocked.Exchange(ref _lastAccessTicks, DateTime.UtcNow.Ticks);
        }

        public bool IsExpired(TimeSpan? expirationTime)
        {
            return expirationTime.HasValue && DateTime.UtcNow - LastAccessTime > expirationTime.Value;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                // Data array will be garbage collected
                _disposed = true;
            }
        }
    }
}
