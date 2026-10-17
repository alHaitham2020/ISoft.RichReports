namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Cache statistics for monitoring
    /// </summary>
    public class ImageCacheStatistics
    {
        public int ItemCount { get; set; }

        public long TotalSizeBytes { get; set; }

        public int MaxItems { get; set; }

        public long MaxSizeBytes { get; set; }

        public long Hits { get; set; }

        public long Misses { get; set; }

        public double HitRatio => (Hits + Misses) > 0 ? (double)Hits / (Hits + Misses) : 0;

        public double UsagePercentage => MaxItems > 0 ? (double)ItemCount / MaxItems * 100 : 0;

        public double SizeUsagePercentage => MaxSizeBytes > 0 ? (double)TotalSizeBytes / MaxSizeBytes * 100 : 0;

        public override string ToString()
        {
            return $"Items: {ItemCount}/{MaxItems}, Size: {TotalSizeBytes}/{MaxSizeBytes} bytes, " +
                   $"Hit Ratio: {HitRatio:P2}, Usage: {UsagePercentage:F1}%";
        }
    }
}
