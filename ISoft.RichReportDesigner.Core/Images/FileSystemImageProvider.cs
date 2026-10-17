using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Provides images from Windows file system
    /// </summary>
    public class FileSystemImageProvider : ImageProviderBase
    {
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff", ".tif", ".ico", ".svg", ".webp"
        };

        private readonly long _maxFileSize;

        public FileSystemImageProvider(long maxFileSize = 10 * 1024 * 1024) : base(ImageUriScheme.File)// 10 MB default
        {
            _maxFileSize = maxFileSize;
        }

        public override bool Exists(string uri)
        {
            return TryGetSecureFilePath(uri, out var filePath) && File.Exists(filePath);
        }

        public override Stream GetStream(string uri)
        {
            if (!TryGetSecureFilePath(uri, out var filePath))
                return null;

            if (!IsValidImageFile(filePath))
                return null;

            try
            {
                return new FileStream(
                    filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize: 4096,
                    useAsync: false); // Sync method should use sync IO
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // Log exception details here
                return null;
            }
        }

        public override async Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken = default)
        {
            if (!TryGetSecureFilePath(uri, out var filePath))
                return null;

            if (!IsValidImageFile(filePath))
                return null;

            try
            {
                return new FileStream(
                    filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize: 4096,
                    useAsync: true);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // Log exception details here
                return null;
            }
        }

        private bool TryGetSecureFilePath(string uri, out string filePath)
        {
            filePath = null;
            if (!ValidateUri(uri, out var path))
            {
                // Also handle direct file paths (backward compatibility)
                if (string.IsNullOrWhiteSpace(uri))
                    return false;

                path = uri.Trim();
            }

            try
            {
                // Get full path to normalize and validate
                filePath = Path.GetFullPath(path);
                return true;
            }
            catch (Exception ex) when (ex is ArgumentException or PathTooLongException)
            {
                return false;
            }
        }

        private bool IsValidImageFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            if (!AllowedExtensions.Contains(extension))
                return false;

            try
            {
                var fileInfo = new FileInfo(filePath);
                return fileInfo.Exists && fileInfo.Length <= _maxFileSize && fileInfo.Length > 0;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
