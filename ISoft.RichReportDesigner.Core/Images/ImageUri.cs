using ISoft.Office.Barcode;
using System;
using System.ComponentModel;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Represents a complete image URI combining a scheme and path.
    /// This immutable struct provides type-safe handling of image URIs with scheme validation.
    /// </summary>
    [TypeConverter(typeof(ImageUriConverter))]
    public readonly struct ImageUri : IEquatable<ImageUri>, IImageUri
    {
        #region Static Instances
        /// <summary>Represents an empty/uninitialized image URI.</summary>
        public static readonly ImageUri Empty = new(ImageUriScheme.Empty, string.Empty);
        #endregion

        #region Fields & Properties
        private readonly ImageUriScheme _scheme;
        private readonly string _path;

        /// <summary>
        /// Gets the URI scheme (e.g., Base64, File, Resource).
        /// </summary>
        public ImageUriScheme Scheme => _scheme;

        /// <summary>
        /// Gets the path portion of the URI without the scheme prefix.
        /// </summary>
        public string Path => _path;

        /// <summary>
        /// Gets the full URI string including scheme prefix and path.
        /// Cached on first access for performance.
        /// </summary>
        public string FullUri => IsEmpty ? string.Empty : string.Concat(_scheme.Prefix, _path);

        /// <summary>
        /// Indicates whether this URI is empty/uninitialized.
        /// </summary>
        public bool IsEmpty => _scheme.IsEmpty && string.IsNullOrEmpty(_path);

        /// <summary>
        /// Indicates whether this URI has a valid scheme and non-empty path.
        /// </summary>
        public bool IsValid => !_scheme.IsEmpty && !string.IsNullOrWhiteSpace(_path);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new ImageUri with the specified scheme and path.
        /// </summary>
        /// <param name="scheme">The URI scheme.</param>
        /// <param name="path">The path portion.</param>
        public ImageUri(ImageUriScheme scheme, string path)
        {
            _scheme = scheme;
            _path = path ?? string.Empty;
        }

        /// <summary>
        /// Creates a new ImageUri from a full URI string.
        /// Throws FormatException if the URI is invalid.
        /// </summary>
        /// <param name="uri">The full URI string (e.g., "base64://abc123").</param>
        public ImageUri(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                _scheme = ImageUriScheme.Empty;
                _path = string.Empty;
                return;
            }

            if (!ImageUriScheme.TryParse(uri, out _scheme, out _path))
            {
                throw new FormatException($"Invalid image URI format: '{uri}'. Expected format: 'scheme://path'");
            }
        }

        #endregion

        #region Static Factory Methods
        /// <summary>
        /// Creates an ImageUri for base64 encoded image data.
        /// </summary>
        /// <param name="base64Data">The base64 encoded string.</param>
        public static ImageUri CreateBase64(byte[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Data cannot be null or empty", nameof(data));

            return CreateBase64(Convert.ToBase64String(data));
        }

        /// <summary>
        /// Creates an ImageUri for base64 encoded image data.
        /// </summary>
        /// <param name="base64Data">The base64 encoded string.</param>
        public static ImageUri CreateBase64(string base64Data)
        {
            if (string.IsNullOrWhiteSpace(base64Data))
                throw new ArgumentException("Base64 data cannot be null or empty", nameof(base64Data));

            return ImageUriScheme.Base64.CreateUri(base64Data);
        }

        /// <summary>
        /// Creates an ImageUri for a file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public static ImageUri CreateFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            // Normalize path separators and ensure absolute path
            var normalizedPath = NormalizeFilePath(filePath);

            return ImageUriScheme.File.CreateUri(normalizedPath);
        }

        /// <summary>
        /// Creates an ImageUri for an embedded resource.
        /// </summary>
        /// <param name="fullPath">The resource path.</param>
        public static ImageUri CreateResource(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                throw new ArgumentException("Resource path cannot be null or empty", nameof(fullPath));

            return ImageUriScheme.Resource.CreateUri(fullPath);
        }

        /// <summary>
        /// Creates an ImageUri for an embedded resource.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        public static ImageUri CreateResource(string assemblyName, string resourcePath)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
                throw new ArgumentException("Assembly name cannot be null or empty", nameof(assemblyName));
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException("Resource path cannot be null or empty", nameof(resourcePath));

            return ImageUriScheme.Resource.CreateUri($"{assemblyName}/{resourcePath.TrimStart('/')}");
        }

        public static ImageUri CreateBarcode(string content, BarcodeType barcodeType)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("barcode data cannot be null or empty", nameof(content));

            var path = BarcodeUriHelper.CreateUriPath(barcodeType, content);

            return ImageUriScheme.Barcode.CreateUri(path);
        }

        /// <summary>
        /// Creates an ImageUri with a custom scheme.
        /// </summary>
        /// <param name="schemeName">The custom scheme name.</param>
        /// <param name="path">The path portion.</param>
        public static ImageUri CreateCustom(string schemeName, string path)
        {
            if (string.IsNullOrWhiteSpace(schemeName))
                throw new ArgumentException("Scheme name cannot be null or empty", nameof(schemeName));

            var scheme = ImageUriScheme.GetOrCreate(schemeName);
            return scheme.CreateUri(path);
        }

        /// <summary>
        /// Creates an ImageUri with a custom full Uri.
        /// </summary>
        /// <param name="uri">The path portion.</param>
        public static ImageUri CreateCustom(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("uri cannot be null or empty", nameof(uri));

            return new ImageUri(uri);
        }

        /// <summary>
        /// Handles conversion of image field values based on the URI scheme type.
        /// Uses pattern matching for cleaner type checking and conversion.
        /// </summary>
        public static ImageUri CreateFromSchemeType(ImageUriSchemeType schemeType, object value, BarcodeType barcodeType = BarcodeType.QRCode)
        {
            // Use switch expression with pattern matching for cleaner logic
            return schemeType switch
            {
                ImageUriSchemeType.Base64String when value is byte[] bytes && bytes != null && bytes.Length > 0
                    => CreateBase64(bytes),

                ImageUriSchemeType.File when value is string filePath && !string.IsNullOrEmpty(filePath)
                    => CreateFile(filePath),

                ImageUriSchemeType.Resource when value is string resourcePath && !string.IsNullOrEmpty(resourcePath)
                    => CreateResource(resourcePath),

                ImageUriSchemeType.Barcode when value is string barcodeText && !string.IsNullOrEmpty(barcodeText)
              => CreateBarcode(barcodeText, barcodeType),// default as QR

                ImageUriSchemeType.Custom when value is string customPath && !string.IsNullOrEmpty(customPath)
               => CreateCustom(customPath),

                // Return original value if no conversion applies
                _ => throw new NotSupportedException(nameof(schemeType))
            };
        }

        /// <summary>
        /// Normalizes image path for Word field codes
        /// </summary>
        private static string NormalizeFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return path;

            // Convert to absolute path
            //if (!System.IO.Path.IsPathRooted(path))
            //{
            //    path = System.IO.Path.GetFullPath(path);
            //}

            // Replace single backslashes with double backslashes for Word field codes
            return path.Replace(@"\", @"\\");
        }

        #endregion

        #region Parsing Methods
        /// <summary>
        /// Attempts to parse a full URI string into an ImageUri.
        /// </summary>
        /// <param name="uri">The full URI string (e.g., "base64://abc123").</param>
        /// <param name="result">When successful, contains the parsed ImageUri.</param>
        /// <returns>True if parsing was successful.</returns>
        public static bool TryParse(string uri, out ImageUri result)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                result = Empty;
                return false;
            }

            if (ImageUriScheme.TryParse(uri, out var scheme, out var path))
            {
                result = new ImageUri(scheme, path);
                return true;
            }

            result = Empty;
            return false;
        }

        /// <summary>
        /// Parses a full URI string into an ImageUri.
        /// Returns Empty if parsing fails.
        /// </summary>
        public static ImageUri Parse(string uri)
        {
            return TryParse(uri, out var result) ? result : Empty;
        }

        /// <summary>
        /// Attempts to change the scheme of an existing URI.
        /// </summary>
        public static string ChangeUriPath(string uri, string newPath)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("URI cannot be null or empty", nameof(uri));

            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentException("New path cannot be empty", nameof(newPath));

            // Extract the path from the old URI
            if (TryParse(uri, out var result))
            {
                return result.WithPath(newPath);
            }

            // If no scheme found, treat the entire string as path
            return CreateCustom(uri);
        }

        #endregion

        #region Conversion & Manipulation
        /// <summary>
        /// Creates a new ImageUri with the same scheme but different path.
        /// </summary>
        /// <param name="newPath">The new path.</param>
        public ImageUri WithPath(string newPath)
        {
            return new ImageUri(_scheme, newPath);
        }

        /// <summary>
        /// Creates a new ImageUri with the same path but different scheme.
        /// </summary>
        /// <param name="newScheme">The new scheme.</param>
        public ImageUri WithScheme(ImageUriScheme newScheme)
        {
            return new ImageUri(newScheme, _path);
        }

        /// <summary>
        /// Converts this URI to a different scheme if possible.
        /// </summary>
        /// <param name="targetScheme">The target scheme.</param>
        /// <returns>A new ImageUri with the target scheme and same path.</returns>
        public ImageUri ConvertTo(ImageUriScheme targetScheme)
        {
            if (targetScheme.IsEmpty)
                throw new ArgumentException("Target scheme cannot be empty", nameof(targetScheme));

            return new ImageUri(targetScheme, _path);
        }

        /// <summary>
        /// Returns the full URI string representation.
        /// </summary>
        public override string ToString() => FullUri;

        /// <summary>
        /// Implicit conversion to string returns the full URI.
        /// </summary>
        public static implicit operator string(ImageUri uri) => uri.FullUri;
        #endregion

        #region Equality
        /// <summary>
        /// Determines equality with another ImageUri.
        /// Two URIs are equal if they have the same scheme and path.
        /// </summary>
        public bool Equals(ImageUri other)
        {
            return _scheme.Equals(other._scheme) &&
                   string.Equals(_path, other._path, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines equality with another object.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is ImageUri other && Equals(other);
        }

        /// <summary>
        /// Generates a hash code based on scheme and path.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + _scheme.GetHashCode();
                hash = hash * 31 + (_path?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Equality operator for ImageUri.
        /// </summary>
        public static bool operator ==(ImageUri left, ImageUri right) => left.Equals(right);

        /// <summary>
        /// Inequality operator for ImageUri.
        /// </summary>
        public static bool operator !=(ImageUri left, ImageUri right) => !left.Equals(right);

        /// <summary>
        /// Checks if a nullable ImageUri is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(ImageUri? uri) => !uri.HasValue || uri.Value.IsEmpty;
        #endregion

        #region Deconstruction
        /// <summary>
        /// Deconstructs the ImageUri into scheme and path.
        /// </summary>
        public void Deconstruct(out ImageUriScheme scheme, out string path)
        {
            scheme = _scheme;
            path = _path;
        }
        #endregion
    }
}
