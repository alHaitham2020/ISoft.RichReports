using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;
using ISoft.RichReportDesigner.Core.Templates;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichDocumentPropertiesManagerService
    {
        string GetDisplayName(RichDocumentPropertyId id);

        string GetDisplayName(string name);

        void UpdateDocDesignProperties(DocumentProperties properties, IRichReportTemplate template);

        void UpdateDocPreviewProperties(DocumentProperties properties, IRichReportTemplate template);
    }
}
