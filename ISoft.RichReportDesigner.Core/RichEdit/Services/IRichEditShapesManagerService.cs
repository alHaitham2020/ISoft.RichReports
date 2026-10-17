using DevExpress.XtraRichEdit.API.Native;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichEditShapesManagerService
    {
        Shape InsertShape(DocumentPosition position, ShapeGeometryPreset preset);
    }
}
