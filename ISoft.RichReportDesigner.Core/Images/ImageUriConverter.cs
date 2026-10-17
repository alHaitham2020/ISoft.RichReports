using ISoft.Office.Barcode;
using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.ComponentModel;
using System.Globalization;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// TypeConverter for ImageUri that enables conversion between strings and ImageUri instances.
    /// Supports conversion from string to ImageUri and from ImageUri to string.
    /// </summary>
    public class ImageUriConverter : TypeConverter
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
                   destinationType == typeof(ImageUri) ||
                   destinationType == typeof(Uri) ||
                   base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts from the specified value to an ImageUri.
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return ImageUri.Empty;

            switch (value)
            {
                case string stringValue when string.IsNullOrWhiteSpace(stringValue):
                    return ImageUri.Empty;

                case string stringValue:
                    // Try to parse as a full URI first
                    if (ImageUri.TryParse(stringValue, out var imageUri))
                        return imageUri;

                    // If parsing fails, try to create a custom URI with the string as path
                    try
                    {
                        return ImageUri.CreateCustom(stringValue);
                    }
                    catch
                    {
                        // If all fails, return empty
                        return ImageUri.Empty;
                    }

                case Uri uriValue:
                    // Convert from Uri
                    if (ImageUri.TryParse(uriValue.ToString(), out var uriResult))
                        return uriResult;
                    return ImageUri.Empty;

                case ImageUriScheme schemeValue:
                    // Convert from ImageUriScheme to ImageUri with empty path
                    return new ImageUri(schemeValue, string.Empty);

                default:
                    return base.ConvertFrom(context, culture, value);
            }
        }

        /// <summary>
        /// Converts from an ImageUri to the specified destination type.
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ImageUri imageUri)
            {
                if (destinationType == typeof(string))
                    return imageUri.FullUri;

                if (destinationType == typeof(ImageUri))
                    return imageUri;

                if (destinationType == typeof(Uri))
                    return imageUri.IsEmpty ? null : new Uri(imageUri.FullUri);

                if (destinationType == typeof(ImageUriScheme))
                    return imageUri.Scheme;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Returns whether this converter supports properties for editing.
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true; // Expose properties for editing in PropertyGrid
        }

        /// <summary>
        /// Returns a collection of properties for the type this converter is designed for.
        /// </summary>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value is ImageUri imageUri)
            {
                // Create property descriptors for Scheme and Path
                var properties = new PropertyDescriptorCollection(null);

                properties.Add(new ImageUriPropertyDescriptor(
                    nameof(ImageUri.Scheme),
                    "Scheme",
                    "The URI scheme",
                    typeof(ImageUriScheme),
                    imageUri.Scheme,
                    true));

                properties.Add(new ImageUriPropertyDescriptor(
                    nameof(ImageUri.Path),
                    "Path",
                    "The path portion of the URI",
                    typeof(string),
                    imageUri.Path,
                    true));

                properties.Add(new ImageUriPropertyDescriptor(
                    nameof(ImageUri.FullUri),
                    "FullUri",
                    "The complete URI string",
                    typeof(string),
                    imageUri.FullUri,
                    false));

                properties.Add(new ImageUriPropertyDescriptor(
                    nameof(ImageUri.IsValid),
                    "IsValid",
                    "Indicates whether the URI is valid",
                    typeof(bool),
                    imageUri.IsValid,
                    false));

                return properties;
            }

            return base.GetProperties(context, value, attributes);
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
            // Return common schemes as standard values
            var schemes = ImageUriScheme.GetAllSchemes();
            var uris = new System.Collections.ArrayList();

            foreach (var scheme in schemes)
            {
                // Create example URIs for each scheme
                if (scheme == ImageUriScheme.Base64)
                    uris.Add(ImageUri.CreateBase64("example"));
                else if (scheme == ImageUriScheme.File)
                    uris.Add(ImageUri.CreateFile("C:\\example.jpg"));
                else if (scheme == ImageUriScheme.Resource)
                    uris.Add(ImageUri.CreateResource("Example.jpg"));
                else if (scheme == ImageUriScheme.Barcode)
                    uris.Add(ImageUri.CreateBarcode("example", BarcodeType.QRCode));
                else
                    uris.Add(new ImageUri(scheme, "example"));
            }

            return new StandardValuesCollection(uris);
        }

        /// <summary>
        /// Returns whether the collection of standard values returned from GetStandardValues is exclusive.
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false; // Users can enter custom URIs
        }

        /// <summary>
        /// Returns whether this object supports a standard set of values that can be picked from a list.
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // Show dropdown with example URIs
        }

        #region Helper PropertyDescriptor
        /// <summary>
        /// Custom PropertyDescriptor for ImageUri properties.
        /// </summary>
        private class ImageUriPropertyDescriptor : PropertyDescriptor
        {
            private readonly Type _propertyType;
            private readonly object _value;
            private readonly bool _isReadOnly;

            public ImageUriPropertyDescriptor(string name, string displayName, string description, Type propertyType, object value, bool isReadOnly)
                : base(name, new Attribute[] {
                new DisplayNameAttribute(displayName),
                new DescriptionAttribute(description)
                })
            {
                _propertyType = propertyType;
                _value = value;
                _isReadOnly = isReadOnly;
            }

            public override Type ComponentType => typeof(ImageUri);
            public override bool IsReadOnly => _isReadOnly;
            public override Type PropertyType => _propertyType;

            public override bool CanResetValue(object component) => false;
            public override object GetValue(object component) => _value;
            public override void ResetValue(object component) { }
            public override void SetValue(object component, object value) { }
            public override bool ShouldSerializeValue(object component) => false;
        }
        #endregion
    }
}
