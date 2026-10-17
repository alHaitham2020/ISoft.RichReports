using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using ISoft.Printing.Core.Xpf.Helpers;
using ISoft.Utils.Helpers;
using System.Windows;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public static class RichEditPrintPreviewHelper
    {
        public static void ShowPrintDialog(this IRichEditDocumentServer server, bool rightToLeftLayout = true)
        {
            ArgumentGuardHelper.ArgumentNotNull(server, nameof(server));
            XpfComponentLinkPrintHelper.ShowPrintDialog(server as IBasePrintable, rightToLeftLayout);
        }

        public static void Export(this IRichEditDocumentServer server, ExportFormat format, string defaultFileName = null, bool rightToLeftLayout = true)
        {
            ArgumentGuardHelper.ArgumentNotNull(server, nameof(server));
            XpfComponentLinkPrintHelper.Export(server as IBasePrintable, format, defaultFileName, rightToLeftLayout);
        }

        public static void Print(this IRichEditDocumentServer server, bool rightToLeftLayout = true, string printerName = null)
        {
            ArgumentGuardHelper.ArgumentNotNull(server, nameof(server));
            XpfComponentLinkPrintHelper.PrintDirect(server as IBasePrintable, rightToLeftLayout, printerName);
        }

        public static void ShowPrintPreview(this IRichEditControl control, string title = null, bool rightToLeftLayout = true, Window owner = null)
        {
            ArgumentGuardHelper.ArgumentNotNull(control, nameof(control));
            XpfComponentLinkPrintHelper.ShowPrintPreviewWindow(control as IBasePrintable, title, rightToLeftLayout, owner);
        }

        public static void ShowPrintPreview(this IRichEditDocumentServer server, string title = null, bool rightToLeftLayout = true, Window owner = null)
        {
            ArgumentGuardHelper.ArgumentNotNull(server, nameof(server));
            XpfComponentLinkPrintHelper.ShowPrintPreviewWindow(server as IBasePrintable, title, rightToLeftLayout, owner);
        }
    }
}
