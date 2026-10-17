using ISoft.Printing.Core.Data;

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
