using ISoft.RichReportDesigner.Core.Reports.DataModel;
using System.Windows.Media;

namespace ISoft.RichReportDesigner.UI.DataModel
{
    public interface IXpfRichReportTemplatePreviewItem : IRichReportTemplatePreviewItem
    {
        bool IsLoading { get; }

        ImageSource PreviewImage { get; }
    }
}
