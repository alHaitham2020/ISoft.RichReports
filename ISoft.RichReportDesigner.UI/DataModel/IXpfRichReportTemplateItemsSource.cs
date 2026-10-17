using ISoft.RichReportDesigner.Core.Reports.DataModel;
using ISoft.Utils.Data.AsyncData;

namespace ISoft.RichReportDesigner.UI.DataModel
{
    public interface IXpfRichReportTemplateItemsSource : IRichReportTemplateItemsSource
    {
        IAsyncCollectionDataSourceProvider<IXpfRichReportTemplatePreviewItem> Source { get; }
    }
}
