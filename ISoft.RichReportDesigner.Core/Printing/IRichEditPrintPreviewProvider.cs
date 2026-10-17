using DevExpress.XtraRichEdit;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public interface IRichEditPrintPreviewProvider
    {
        void ShowPrintPreviewWindow(IRichEditDocumentServer server, string title = null, bool rightToLeftLayout = true);
    }

}
