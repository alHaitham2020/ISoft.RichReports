using ISoft.Mvvm.ViewModel.Common;

namespace ISoft.RichReportDesigner.UI.ViewModel
{
    public class RichReportViewerDocumentViewModel : DocumentViewModelBase
    {
        public virtual object ReportSource { get; set; }

        public RichReportViewerDocumentViewModel(object reportSource)
        {
            ReportSource = reportSource;
        }
    }
}
