using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.Designer;
using ISoft.RichReportDesigner.Core.Reports.DataModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public interface IRichEditorControl : IRichEditControl
    {
        bool Modified { get; set; }

        float DpiX { get; }

        float DpiY { get; }

        internal DevExpress.XtraRichEdit.Model.DocumentModel DocumentModel { get; }

        DocumentLayout DocumentLayout { get; }

        DocumentLayoutUnit LayoutUnit { get; }

        IRichReportDesigner Owner { get; }

        bool IsSelectionInMainDocument { get; }

        bool FieldHighlightingEnabled { get; }

        T GetLayoutElement<T>(DocumentPosition position) where T : RangedLayoutElement;

        bool CreateNewDocument(bool raiseDocumentClosing = false);

        bool LoadDocumentTemplate(Stream stream, DocumentFormat documentFormat);

        bool LoadDocumentTemplate(Stream stream);

        bool LoadDocumentTemplate(byte[] buffer);

        bool LoadDocumentTemplate(byte[] buffer, DocumentFormat documentFormat);

        byte[] SaveDocument(DocumentFormat documentFormat);

        void SaveDocument(Stream stream, DocumentFormat documentFormat);

        IRichEditDocumentServer CreateDocumentServer();

        void MailMerge(Document document);

        void MailMerge(MailMergeOptions options, Document targetDocument);

        void ExportToImage(Stream stream);

        void ExportToImage(Stream stream, DevExpress.XtraPrinting.ImageExportOptions imageExportOptions);

        byte[] ExportToImage();

        Task ShowPreviewAsync();

        bool CanShowPreview();

        Task SaveAsync();

        bool CanSave();

        System.Drawing.Point PointToScreen(System.Drawing.Point clientPoint);

        DocumentPosition GetPositionFromPoint(PointF clientPoint);

        Rectangle GetBoundsFromPosition(DocumentPosition caretPosition);

        Rectangle GetLayoutLogicalBoundsFromPosition(DocumentPosition position);

        System.Drawing.Point GetPointFromPosition(DocumentPosition position, bool topBound);

        DocumentPosition GetDropPosition(System.Drawing.Point point);

        void ShowCalculatedFieldsForm();

        void ShowCalculatedFieldModelForm(RichReportCalculatedFieldModel model, IRichReportCalculatedFieldModelOwner owner);

        RichEditBrushBase CreateBrush(Color color);

        RichEditPenBase CreatePen(System.Drawing.Color color, int thickness);

        void OnFieldHighlightingEnabledChanged();
    }
}
