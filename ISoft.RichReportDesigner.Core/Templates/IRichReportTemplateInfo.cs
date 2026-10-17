namespace ISoft.RichReportDesigner.Core.Templates
{
    public interface IRichReportTemplateInfo : IRichReportTemplateDataInfo
    {
        int? ReportId {  get;}

        string ReportName { get; }

        string ReportTitle { get;}
    }
}
