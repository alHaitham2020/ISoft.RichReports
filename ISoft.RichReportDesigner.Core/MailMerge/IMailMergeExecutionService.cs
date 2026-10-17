using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.MailMerge
{
    public interface IMailMergeExecutionService
    {
        void ExecuteMailMerge(IRichEditDocumentServer inputServer, MailMergeOptions options, IRichEditDocumentServer outputServer);

        void ExecuteMailMerge(Document sourceDocument, MailMergeOptions options, Document targetDocument);

        Task ExecuteMailMergeAsync(IRichEditDocumentServer inputServer, MailMergeOptions options, IRichEditDocumentServer outputServer);

        Task ExecuteMailMergeAsync(Document sourceDocument, MailMergeOptions options, Document targetDocument);
    }
}
