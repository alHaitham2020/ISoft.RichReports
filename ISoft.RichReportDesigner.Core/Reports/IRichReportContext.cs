using ISoft.Printing.Core.Data;
using ISoft.Printing.Shared.Data;
using ISoft.RichReportDesigner.Core.Data;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;

namespace ISoft.RichReportDesigner.Core.Reports
{
    /// <summary>
    /// Report context containing all required dependencies
    /// </summary>
    public interface IRichReportContext
    {
        IReportInfo ReportInfo { get; }

        IRichReportDataSource DataSource { get; }

        IRichReportTemplateStorage TemplateStorage { get; }

        IRichReportCalculatedFieldStorage CalculatedFieldStorage { get; }

        IRichReportPrintPermissionInfo PermissionInfo { get; }

        IRichReportProperties Properties { get; }

        IRichDocumentProperties DocumentProperties { get; }

        IRichDocumentCustomProperties DocumentCustomProperties { get; }

        IRichDocumentVariablesProperties DocumentVariableProperties { get; }

        IRichReportTemplateInfoProvider TemplateInfoProvider { get; }

        IRichDefaultTemplateInfoProvider DefaultTemplateInfoProvider { get; }
    }
}
