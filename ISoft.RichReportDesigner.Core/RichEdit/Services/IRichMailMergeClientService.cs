using DevExpress.XtraRichEdit;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichMailMergeClientService
    {
        void Started(MailMergeStartedEventArgs e);

        void Finished(MailMergeFinishedEventArgs e);

        void RecordStarted(MailMergeRecordStartedEventArgs e);

        void RecordFinished(MailMergeRecordFinishedEventArgs e);
    }
}
