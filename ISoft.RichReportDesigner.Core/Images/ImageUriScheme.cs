using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Represents an immutable URI scheme for handling different image source types.
    /// This struct is optimized for performance and memory efficiency as a value type.
    /// Supports both known and unknown schemes dynamically.
    /// </summary>
    [TypeConverter(typeof(ImageUriSchemeConverter))]
    public readonly struct ImageUriScheme : IEquatable<ImageUriScheme>, IImageUriScheme
    {
        #region Pre-allocated Common Schemes
        public static readonly ImageUriScheme Empty = new(string.Empty, ImageUriSchemeType.None);

        public static readonly ImageUriScheme Base64 = new(ImageUriSchemes.Base64, ImageUriSchemeType.Base64String);

        public static readonly ImageUriScheme File = new(ImageUriSchemes.File, ImageUriSchemeType.File);

        public static readonly ImageUriScheme Resource = new(ImageUriSchemes.Resource, ImageUriSchemeType.Resource);

        public static readonly ImageUriScheme Barcode = new(ImageUriSchemes.Barcode, ImageUriSchemeType.Barcode);
        #endregion

        #region Fields & Properties
        /// <summary>
        /// Fast lookup dictionary for known schemes (initialized once).
        /// Uses case-insensitive comparison for RFC compliance.
        /// </summary>
        private static readonly ImageUriSchemeCollection KnownSchemesLookup = new()
        {
            [ImageUriSchemes.Base64] = Base64,
            [ImageUriSchemes.File] = File,
            [ImageUriSchemes.Resource] = Resource,
            [ImageUriSchemes.Barcode] = Barcode
        };

        private readonly string _name;
        private readonly string _prefix; // Pre-computed prefix for performance
        private readonly ImageUriSchemeType _type;

        /// <summary>
        /// Gets the scheme name (e.g., "base64", "file", "resource").
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets the full prefix including the scheme suffix (e.g., "base64://").
        /// </summary>
        public string Prefix => _prefix;

        public ImageUriSchemeType Type => _type;

        /// <summary>
        /// Indicates whether this scheme is empty/uninitialized.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_name);

        /// <summary>
        /// Indicates whether this is a known/pre-defined scheme.
        /// </summary>
        public bool IsKnownScheme => !IsEmpty && KnownSchemesLookup.ContainsKey(_name);

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance with the specified scheme name.
        /// The prefix is pre-computed to avoid repeated concatenation during URI parsing.
        /// </summary>
        /// <param name="name">The scheme name (without suffix).</param>
        public ImageUriScheme(string name, ImageUriSchemeType schemeType)
        {
            _name = name ?? string.Empty;
            _prefix = string.IsNullOrEmpty(name)
                ? string.Empty
                : string.Concat(name, ImageUriSchemes.SchemeSuffix);
            _type = schemeType;
        }
        #endregion

        #region Static Factory & Parsing Methods

        /// <summary>
        /// Gets or creates an ImageUriScheme instance. Returns cached instance for known schemes
        /// or previously created dynamic schemes.
        /// </summary>
        public static ImageUriScheme GetOrCreate(string schemeName)
        {
            if (string.IsNullOrWhiteSpace(schemeName))
                return Empty;

            // Check known schemes first (O(1) lookup)
            if (KnownSchemesLookup.TryGetValue(schemeName, out var knownScheme))
                return knownScheme;

            return new ImageUriScheme(schemeName, ImageUriSchemeType.Custom);
        }

        /// <summary>
        /// Attempts to parse a URI string and extract both the scheme and its path.
        /// Works with any valid URI scheme, not just known ones.
        /// </summary>
        /// <param name="uri">The full URI string (e.g., "base64://abc123" or "custom://data").</param>
        /// <param name="scheme">When successful, contains the matched scheme.</param>
        /// <param name="path">When successful, contains the extracted path without the scheme prefix.</param>
        /// <returns>True if the URI contained a valid scheme and path.</returns>
        /// <example>
        /// // Known scheme
        /// ImageUriScheme.TryParse("base64://abc123", out var scheme, out var path);
        /// // scheme == ImageUriScheme.Base64, path == "abc123"
        /// 
        /// // Unknown scheme
        /// ImageUriScheme.TryParse("custom://data", out var customScheme, out var customPath);
        /// // customScheme.Name == "custom", customPath == "data"
        /// </example>
        public static bool TryParse(string uri, out ImageUriScheme scheme, out string path)
        {
            scheme = Empty;
            path = null;

            if (string.IsNullOrWhiteSpace(uri))
                return false;

            ReadOnlySpan<char> uriSpan = uri.AsSpan().Trim();
            int separatorIndex = uriSpan.IndexOf(ImageUriSchemes.SchemeSuffix.AsSpan());

            // Must have "://" separator and scheme name before it
            if (separatorIndex <= 0)
                return false;

            // Extract and validate scheme name
            ReadOnlySpan<char> schemeSpan = uriSpan.Slice(0, separatorIndex);
            if (!IsValidSchemeName(schemeSpan))
                return false;

            // Extract path after separator
            ReadOnlySpan<char> pathSpan = uriSpan.Slice(separatorIndex + ImageUriSchemes.SchemeSuffix.Length).Trim();

            // Validate path is not empty
            if (pathSpan.IsEmpty)
                return false;

            scheme = GetOrCreate(schemeSpan.ToString());
            path = pathSpan.ToString();
            return true;
        }

        /// <summary>
        /// Attempts to parse a URI string and extract the scheme only.
        /// </summary>
        public static bool TryParseScheme(string uri, out ImageUriScheme scheme)
        {
            return TryParse(uri, out scheme, out _);
        }

        /// <summary>
        /// Parses a URI and returns the scheme, or Empty if parsing fails.
        /// </summary>
        public static ImageUriScheme Parse(string uri)
        {
            return TryParse(uri, out var scheme, out _) ? scheme : Empty;
        }

        /// <summary>
        /// Attempts to change the scheme of an existing URI.
        /// </summary>
        public static string ChangeUriScheme(string uri, ImageUriScheme newScheme)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("URI cannot be null or empty", nameof(uri));

            if (newScheme.IsEmpty)
                throw new ArgumentException("New scheme cannot be empty", nameof(newScheme));

            // Extract the path from the old URI
            if (TryParse(uri, out _, out var path))
            {
                return newScheme.CreateUriString(path);
            }

            // If no scheme found, treat the entire string as path
            return newScheme.CreateUriString(uri);
        }

        /// <summary>
        /// Checks if a scheme is supported
        /// </summary>
        public static bool IsSupported(string scheme) => KnownSchemesLookup.ContainsKey(scheme);
        #endregion

        #region Validation
        /// <summary>
        /// Validates a scheme name according to RFC 3986.
        /// Scheme names must start with a letter and contain only letters, digits, '+', '-', or '.'.
        /// </summary>
        private static bool IsValidSchemeName(ReadOnlySpan<char> schemeName)
        {
            if (schemeName.IsEmpty)
                return false;

            // First character must be a letter
            if (!char.IsLetter(schemeName[0]))
                return false;

            // Remaining characters must be valid scheme characters
            for (int i = 1; i < schemeName.Length; i++)
            {
                char c = schemeName[i];
                if (!char.IsLetterOrDigit(c) && c != '+' && c != '-' && c != '.')
                    return false;
            }

            return true;
        }
        #endregion

        #region Scheme Management
        /// <summary>
        /// Gets all currently registered schemes (both known and dynamically created).
        /// </summary>
        public static ICollection<ImageUriScheme> GetAllSchemes()
        {
            return KnownSchemesLookup.Values;
        }

        /// <summary>
        /// Gets all currently registered schemes (both known and dynamically created).
        /// </summary>
        public static ImageUriScheme[] GetAllSchemesValues()
        {
            return KnownSchemesLookup.Values.ToArray();
        }

        #endregion

        #region Instance Methods
        /// <summary>
        /// Determines whether this scheme can handle the specified URI.
        /// Uses case-insensitive comparison for RFC compliance.
        /// </summary>
        public bool CanHandle(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri) || IsEmpty)
                return false;

            return uri.AsSpan().Trim().StartsWith(_prefix.AsSpan(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Attempts to extract the path portion from a scheme-prefixed URI.
        /// </summary>
        public bool TryParsePath(string uri, out string path)
        {
            path = null;

            if (!CanHandle(uri))
                return false;

            ReadOnlySpan<char> uriSpan = uri.AsSpan().Trim();
            int startIndex = _prefix.Length;

            if (startIndex >= uriSpan.Length)
            {
                path = string.Empty;
                return true;
            }

            ReadOnlySpan<char> pathSpan = uriSpan.Slice(startIndex).Trim();
            path = pathSpan.ToString();
            return !string.IsNullOrEmpty(path);
        }

        /// <summary>
        /// Creates a full URI string with the given path using this scheme.
        /// </summary>
        public string CreateUriString(string path)
        {
            if (IsEmpty)
                throw new InvalidOperationException("Cannot create URI with empty scheme");

            return CreateUri(path);
        }

        /// <summary>
        /// Creates an ImageUri instance from this scheme and a path.
        /// </summary>
        public ImageUri CreateUri(string path)
        {
            return new ImageUri(this, path);
        }

        #endregion

        #region Equality & Conversion
        public override string ToString() => _name;

        public override bool Equals(object obj) => obj is ImageUriScheme other && Equals(other);

        public bool Equals(ImageUriScheme other) =>
            string.Equals(_name, other._name, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() => _name?.GetHashCode() ?? 0;

        public static bool operator ==(ImageUriScheme left, ImageUriScheme right) => left.Equals(right);
        public static bool operator !=(ImageUriScheme left, ImageUriScheme right) => !left.Equals(right);

        public static bool IsNullOrEmpty(ImageUriScheme? scheme) => !scheme.HasValue || scheme.Value.IsEmpty;

        /// <summary>
        /// Implicit conversion from string to ImageUriScheme for convenience.
        /// </summary>
        public static implicit operator ImageUriScheme(string schemeName) => GetOrCreate(schemeName);

        /// <summary>
        /// Implicit conversion to string returns the full URI.
        /// </summary>
        public static implicit operator string(ImageUriScheme uri) => uri.Name;
        #endregion
    }
}
