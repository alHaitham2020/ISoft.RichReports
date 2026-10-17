using ISoft.RichReportDesigner.Core.Templates;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportTemplateDataProviderService
    {
        IRichReportTemplateData GetTemplateByKey(Guid? templateId);

        Task<IRichReportTemplateData> GetTemplateByKeyAsync(Guid? templateId);
    }
}
