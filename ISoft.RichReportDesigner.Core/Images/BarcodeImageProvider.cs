using DevExpress.Drawing;
using ISoft.Office.Barcode;
using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.IO;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Provides images from barcode strings
    /// </summary>
    public class BarcodeImageProvider : ImageProviderBase
    {
        public BarcodeImageProvider() : base(ImageUriScheme.Barcode) { }

        public override bool Exists(string uri) => !string.IsNullOrEmpty(uri);

        public override Stream GetStream(string uri)
        {
            if (!ValidateUri(uri, out var path) || !BarcodeUriHelper.TryParseUriPath(path, out var barcodeType, out var text))
                return null;

            try
            {
                using var generator = BarcodeGeneratorFactory.Create(text, barcodeType.ToSymbology());
                return generator.GenerateStream(DXImageFormat.Png);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
