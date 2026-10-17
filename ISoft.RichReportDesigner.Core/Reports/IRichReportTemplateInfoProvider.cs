using ISoft.RichReportDesigner.Core.Templates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportTemplateInfoProvider
    {
        IRichReportTemplateInfo GetByKey(Guid? templateId);

        Task<IRichReportTemplateInfo> GetByKeyAsync(Guid? templateId);

        IEnumerable<IRichReportTemplateInfo> GetReportTemplates(int? reportId, RichReportTemplateSourceType sourceType);

        Task<IEnumerable<IRichReportTemplateInfo>> GetReportTemplatesAsync(int? reportId, RichReportTemplateSourceType sourceType);

        Task<IEnumerable<IRichReportTemplateInfo>> GetAllTemplatesAsync();
    }

}
