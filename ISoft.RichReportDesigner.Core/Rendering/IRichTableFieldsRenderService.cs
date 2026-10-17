using DevExpress.XtraRichEdit.API.Native;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Rendering
{
    public interface IRichTableFieldsRenderService
    {
        void Render(Document document);
        Task RenderAsync(Document document, CancellationToken cancellationToken = default);
    }
}
