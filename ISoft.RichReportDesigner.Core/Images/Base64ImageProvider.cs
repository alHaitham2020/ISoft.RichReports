using System;
using System.IO;

namespace ISoft.RichReportDesigner.Core.Images
{ 
    /// <summary>
    /// Provides images from Base64 encoded strings
    /// </summary>
    public class Base64ImageProvider : ImageProviderBase
    {
        public Base64ImageProvider() : base(ImageUriScheme.Base64)
        {
            
        }

        public override bool Exists(string uri)
        {
            return TryGetBase64Data(uri, out var data) && data.Length > 0;
        }

        public override Stream GetStream(string uri)
        {
            if (!TryGetBase64Data(uri, out var base64Data))
                return null;

            try
            {
                return ConvertToStream(base64Data);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private bool TryGetBase64Data(string uri, out string base64Data)
        {
            base64Data = null;

            if (!ValidateUri(uri, out var data))
                return false;

            try
            {
                // Try to decode to validate
                Convert.FromBase64String(data);
                base64Data = data;
                return true;
            }
            catch (FormatException)
            {
                // Try URL-decoded version
                try
                {
                    data = Uri.UnescapeDataString(data);
                    Convert.FromBase64String(data);
                    base64Data = data;
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
        }

        private static MemoryStream ConvertToStream(string base64String)
        {
            var buffer = Convert.FromBase64String(base64String);
            return new MemoryStream(buffer, writable: false);
        }
    }
}
