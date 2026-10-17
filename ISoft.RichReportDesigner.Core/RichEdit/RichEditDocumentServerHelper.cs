using DevExpress.XtraRichEdit;
using ISoft.Utils.Helpers;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public static class RichEditDocumentServerHelper
    {
        public static bool LoadDocument(this IRichEditDocumentServer server, byte[] data, DocumentFormat format)
        {
            return StreamSerializationHelper.DeserializeFromBytes(data, s =>
            {
                return server.LoadDocument(s, format);
            });
        }

        public static bool LoadDocumentTemplate(this IRichEditDocumentServer server, byte[] data, DocumentFormat format)
        {
            return StreamSerializationHelper.DeserializeFromBytes(data, s =>
            {
                return server.LoadDocumentTemplate(s, format);
            });
        }
    }
}
