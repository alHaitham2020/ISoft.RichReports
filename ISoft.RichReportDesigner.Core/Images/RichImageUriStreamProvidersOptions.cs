using ISoft.Utils.Data;
using System;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Configuration options for RichImageUriStreamProvidersManager
    /// </summary>
    public class RichImageUriStreamProvidersOptions
    {
        public bool EnableCaching { get; set; } = true;

        public int MaxCacheItems { get; set; } = 50;

        public long MaxCacheSizeBytes { get; set; } = DataSize.FromMegabytes(50); // 50 MB

        public TimeSpan? CacheExpirationTime { get; set; } = TimeSpan.FromMinutes(10);

        public bool WarnOnProviderOverride { get; set; } = true;

        public bool EnableErrorLogging { get; set; } = true;

        public Action OnCacheCleared { get; set; }
    }
}
