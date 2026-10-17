using DevExpress.XtraRichEdit.API.Native;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichDocumentCustomPropertiesManagerService
    {
        string GetDisplayName(string propertyName);

        void UpdateDocCustomProperties(DocumentCustomProperties properties);
    }
}
