using ISoft.Printing.Core.Data;
using ISoft.Printing.Shared.Data;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportTemplateSourceFilterOptions
    {
        IReportInfo ReportInfo { get;}

        RichReportTemplateSourceType SourceType { get;}

        bool IsValid();

        void Validate();
    }
}
