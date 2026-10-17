using System;
using System.ComponentModel;
using System.Globalization;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// TypeConverter for ImageUriScheme that enables comprehensive conversion support.
    /// Supports conversion from strings, URIs, and provides standard property editor support.
    /// </summary>
    public class ImageUriSchemeConverter : TypeConverter
    {
        /// <summary>
        /// Determines if this converter can convert from the specified source type.
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) ||
                   sourceType == typeof(Uri) ||
                   base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Determines if this converter can convert to the specified destination type.
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) ||
                   destinationType == typeof(ImageUriScheme) ||
                   destinationType == typeof(Uri) ||
                   base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts from the specified value to an ImageUriScheme.
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return ImageUriScheme.Empty;

            switch (value)
            {
                case string stringValue when string.IsNullOrWhiteSpace(stringValue):
                    return ImageUriScheme.Empty;

                case string stringValue:
                    // Try to parse as a complete URI first
                    if (ImageUriScheme.TryParseScheme(stringValue, out var scheme))
                        return scheme;

                    // If parsing fails, treat as a scheme name directly
                    return ImageUriScheme.GetOrCreate(stringValue.Trim());

                case Uri uriValue:
                    // Convert from Uri
                    if (ImageUriScheme.TryParseScheme(uriValue.ToString(), out var uriScheme))
                        return uriScheme;
                    return ImageUriScheme.Empty;

                default:
                    return base.ConvertFrom(context, culture, value);
            }
        }

        /// <summary>
        /// Converts from an ImageUriScheme to the specified destination type.
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ImageUriScheme scheme)
            {
                if (destinationType == typeof(string))
                    return scheme.Name;

                if (destinationType == typeof(ImageUriScheme))
                    return scheme;

                if (destinationType == typeof(Uri))
                    return scheme.IsEmpty ? null : new Uri(scheme.CreateUriString(string.Empty));
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Returns whether this converter supports properties for editing.
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false; // We don't expose properties for editing
        }

        /// <summary>
        /// Returns whether changing a value on this object requires a call to CreateInstance.
        /// </summary>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return false; // We can modify existing instances
        }

        /// <summary>
        /// Returns a collection of standard values for the type this converter is designed for.
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // Return all known schemes as standard values
            var schemes = ImageUriScheme.GetAllSchemesValues();
            return new StandardValuesCollection(schemes);
        }

        /// <summary>
        /// Returns whether the collection of standard values returned from GetStandardValues is exclusive.
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false; // Users can enter custom schemes
        }

        /// <summary>
        /// Returns whether this object supports a standard set of values that can be picked from a list.
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // Show dropdown with known schemes
        }
    }
}
