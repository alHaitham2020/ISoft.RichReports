using ISoft.Printing.Shared.Data;
using ISoft.RichReportDesigner.Core.Data;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportContextBuilder
    {
        IRichReportContext Build();

        IRichReportContextBuilder WithReportInfo(IReportInfo reportInfo);

        IRichReportContextBuilder WithDataSource(IRichReportDataSource dataSource);

        IRichReportContextBuilder WithDefaultTemplateInfoProvider(IRichDefaultTemplateInfoProvider provider);

        IRichReportContextBuilder WithPermissionsInfo(IRichReportPrintPermissionInfo permissions);

        IRichReportContextBuilder WithProperties(IRichReportProperties properties);

        IRichReportContextBuilder WithCustomProperties(IRichDocumentCustomProperties properties);

        IRichReportContextBuilder WithTemplateInfoProvider(IRichReportTemplateInfoProvider provider);

        IRichReportContextBuilder WithTemplateStorage(IRichReportTemplateStorage storage);

        IRichReportContextBuilder WithDocumentProperties(IRichDocumentProperties documentProperties);

        IRichReportContextBuilder WithVariableProperties(IRichDocumentVariablesProperties variableProperties);

        IRichReportContextBuilder WithCalculatedFieldInfoProvider(IRichReportCalculatedFieldInfoProvider provider);
    }
}
