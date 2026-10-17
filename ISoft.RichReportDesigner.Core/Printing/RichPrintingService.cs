using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using ISoft.Printing.Core.Xpf.Helpers;
using System;

namespace ISoft.RichReportDesigner.Core.Printing
{
    /// <summary>
    /// Service responsible for printing and exporting documents
    /// </summary>
    public class RichPrintingService : IRichPrintingService
    {
        public RichPrintingService()
        {
            
        }

        /// <summary>
        /// Shows print preview for document content
        /// </summary>
        public void ShowPrintPreview(IBasePrintable printable, string title = null)
        {
            if (printable == null) throw new ArgumentNullException(nameof(printable));

            printable.ShowPrintPreviewWindow(title);
        }

        public void ShowPrintDialog(IRichEditDocumentServer printable)
        {
            if (printable == null) throw new ArgumentNullException(nameof(printable));

            printable.ShowPrintDialog();
        }

        public void Print(IRichEditDocumentServer printable)
        {
            if (printable == null) throw new ArgumentNullException(nameof(printable));

            printable.Print();
        }

        public void Export(IRichEditDocumentServer printable, ExportFormat format, string defaultFileName = null)
        {
            if (printable == null) throw new ArgumentNullException(nameof(printable));

            printable.Export(format, defaultFileName);
        }
    }
}
