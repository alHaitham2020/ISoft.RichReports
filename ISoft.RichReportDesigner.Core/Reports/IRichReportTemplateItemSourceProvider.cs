using ISoft.RichReportDesigner.Core.Reports.DataModel;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportTemplateItemSourceProvider
    {
        IRichReportTemplateItemsSource DefaultTemplatesSource { get; }

        IRichReportTemplateItemsSource StoreTemplatesSource { get; }
    }

    public class RichReportTemplateItemSourceProvider : IRichReportTemplateItemSourceProvider
    {
        public IRichReportTemplateItemsSource DefaultTemplatesSource { get; private set; }

        public IRichReportTemplateItemsSource StoreTemplatesSource { get; private set; }
    }
}
