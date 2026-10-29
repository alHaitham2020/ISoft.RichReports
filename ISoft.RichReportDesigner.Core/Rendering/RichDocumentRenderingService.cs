using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.RichEdit.Services;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Rendering
{
    public class RichDocumentRenderingService : IRichDocumentRenderingService
    {
        private readonly IRichTableFieldsRenderService _renderService;

        public RichDocumentRenderingService(IRichTableFieldsRenderService tableFieldsRenderService)
        {
            _renderService = tableFieldsRenderService ?? throw new System.ArgumentNullException(nameof(tableFieldsRenderService));
        }

        public RichDocumentRenderingService(IRichFieldManagerService fieldManagerService) 
            : this(new RichTableFieldsRenderService(fieldManagerService))
        {

        }

        public void Render(Document doc)
        {
            _renderService.Render(doc);
        }

        public async Task RenderAsync(Document doc)
        {
           await _renderService.RenderAsync(doc);
        }
    }
}
