using DevExpress.XtraRichEdit.API.Native;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Rendering
{
    public interface IRichDocumentRenderingService
    {
        void Render(Document doc);

        Task RenderAsync(Document doc);
    }
}
