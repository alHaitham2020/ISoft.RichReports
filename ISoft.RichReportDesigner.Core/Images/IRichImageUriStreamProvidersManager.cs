using DevExpress.Office.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Images
{
    public interface IRichImageUriStreamProvidersManager : IUriStreamProvider, IDisposable
    {
        int CacheCount { get; }

        long CacheSizeInBytes { get; }

        bool IsCachingEnabled { get; }

        IReadOnlyDictionary<string, IImageSourceProvider> Providers { get; }

        void ClearCache();

        bool Exists(string uri);

        ImageCacheStatistics GetCacheStatistics();

        Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken = default);

        int InvalidateCache(Func<string, bool> predicate);

        bool InvalidateCache(string uri);

        Task<bool> PreloadAsync(string uri, CancellationToken cancellationToken = default);

        RichImageUriStreamProvidersManager RegisterProvider(IImageSourceProvider provider);

        RichImageUriStreamProvidersManager RegisterProviders(params IImageSourceProvider[] providers);

        RichImageUriStreamProvidersManager SetDefaultProvider(IImageSourceProvider provider);

        bool UnregisterProvider(string scheme);
    }
}
