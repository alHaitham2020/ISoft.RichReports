using ISoft.RichReportDesigner.Core.RichEdit;
using ISoft.RichReportDesigner.Core.RichEdit.Services;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public interface IRichPrintable
    {
        IRichWaitIndicationService WaitIndicationService { get; }

        IRichTemplatePreviewServerFactory TemplateServerFactory { get; }
    }
}
