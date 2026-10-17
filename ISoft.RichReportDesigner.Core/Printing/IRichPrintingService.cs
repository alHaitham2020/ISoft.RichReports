using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public interface IRichPrintingService
    {
        void Export(IRichEditDocumentServer printable, ExportFormat format, string defaultFileName = null);

        void Print(IRichEditDocumentServer printable);

        void ShowPrintDialog(IRichEditDocumentServer printable);

        void ShowPrintPreview(IBasePrintable printable, string title = null);
    }
}
