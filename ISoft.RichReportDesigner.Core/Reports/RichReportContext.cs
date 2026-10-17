using ISoft.Printing.Core.Data;
using ISoft.RichReportDesigner.Core.Data;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public class RichReportContext : IRichReportContext
    {
        public IReportInfo ReportInfo { get; set; }

        public IRichDataSource DataSource { get; set; }

        public IRichReportTemplateStorage TemplateStorage { get; set; }

        public IRichReportPrintPermissionInfo PermissionInfo {  get; set; }

        public IRichReportProperties Properties { get; set; }

        public IRichDocumentProperties DocumentProperties { get; set; }

        public IRichDocumentCustomProperties DocumentCustomProperties {  get; set; }

        public IRichDocumentVariablesProperties DocumentVariableProperties { get; set; }

        public IRichReportTemplateInfoProvider TemplateInfoProvider {  get; set; }

        public IRichDefaultTemplateInfoProvider DefaultTemplateInfoProvider { get; set; }

        public IRichReportCalculatedFieldStorage CalculatedFieldStorage {  get; set; }
    }
}
