using ISoft.Office.Barcode;
using ISoft.Utils.Helpers;

namespace ISoft.RichReportDesigner.Core.Images
{
    public static class BarcodeUriHelper
    {
        private const char Seprator = ';';

        public static string CreateUriPath(BarcodeType barcodeType, string text)
            => $"{barcodeType}{Seprator}{text}";

        public static bool TryParseUriPath(string uriPath, out BarcodeType barcodeType, out string text)
        {
            barcodeType = BarcodeType.None;
            text = null;

            if (string.IsNullOrWhiteSpace(uriPath))
                return false;

            var parts = uriPath.Split([Seprator], 2);

            if (parts.Length > 0)
                barcodeType = parts[0].TryConvert(BarcodeType.None);

            if (parts.Length > 1)
                text = parts[1];

            return barcodeType != BarcodeType.None && !string.IsNullOrEmpty(text);
        }
    }
}
