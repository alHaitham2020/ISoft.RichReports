using System;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Defines URI schemes for different image sources
    /// </summary>
    public static class ImageUriSchemes
    {
        /// <summary>
        /// Standardize on "://" for all schemes for consistency
        /// </summary>
        public const string SchemeSuffix = "://";

        /// <summary>
        /// Windows file system: file:///C:/Images/logo.png
        /// </summary>
        public const string File = "file";

        /// <summary>
        /// Application embedded resource: resource://MyAssembly/Images/logo.png
        /// </summary>
        public const string Resource = "resource";

        /// <summary>
        /// Base64 encoded image: base64://iVBORw0KGgo...
        /// </summary>
        public const string Base64 = "base64";

        /// <summary>
        /// Base64 encoded image: base64://iVBORw0KGgo...
        /// </summary>
        public const string Barcode = "barcode";

        public static string GetSchemeName(this ImageUriSchemeType schemeType, string custom = null)
        {
            return schemeType switch
            {
                ImageUriSchemeType.Base64String => Base64,
                ImageUriSchemeType.File => File,
                ImageUriSchemeType.Resource => Resource,
                ImageUriSchemeType.Barcode => Barcode,
                ImageUriSchemeType.Custom => custom,
                ImageUriSchemeType.None => throw new ArgumentException("Scheme type cannot be None", nameof(schemeType)),
                _ => throw new ArgumentOutOfRangeException(nameof(schemeType), schemeType, "Unsupported scheme type")
            };
        }

        public static ImageUriSchemeType GetSchemeType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ImageUriSchemeType.None;

            return name.ToLower() switch
            {
                Base64 => ImageUriSchemeType.Base64String,
                File => ImageUriSchemeType.File,
                Resource => ImageUriSchemeType.Resource,
                Barcode => ImageUriSchemeType.Barcode,
                _ => ImageUriSchemeType.Custom,
            };
        }
    }
}
