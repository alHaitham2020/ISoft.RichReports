namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportProperties
    {
        string DatabaseName { get; }

        string FileName { get; }

        string ServerName { get; }

        string UserName { get; }

        string AppVersion {  get; }
    }
}
