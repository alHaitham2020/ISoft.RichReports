using DevExpress.DataAccess.Native.ExpressionEditor;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Internal;
using DevExpress.XtraRichEdit.Printing;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    internal class RichInnerRichEditControl : SimpleInnerRichEditControl
    {
        public RichInnerRichEditControl(IInnerRichEditControlOwner owner) : base(owner)
        {

        }

        protected override RichEditPrinter CreateRichEditPrinter()
        {
            return base.CreateRichEditPrinter();
        }
      
        public override void BeginInitialize()
        {
            base.BeginInitialize();
            Options.DocumentCapabilities.Bookmarks = DocumentCapability.Default;
            Options.DocumentCapabilities.Tables = DocumentCapability.Default;
            Options.DocumentCapabilities.Sections = DocumentCapability.Default;
            Options.DocumentCapabilities.Numbering.Simple = DocumentCapability.Default;
            Options.DocumentCapabilities.Numbering.MultiLevel = DocumentCapability.Default;
            Options.DocumentCapabilities.Numbering.Bulleted = DocumentCapability.Default;
            Options.DocumentCapabilities.InlinePictures = DocumentCapability.Default;
            Options.DocumentCapabilities.HeadersFooters = DocumentCapability.Default;
            Options.DocumentCapabilities.Hyperlinks = DocumentCapability.Default;
            Options.DocumentCapabilities.ParagraphStyle = DocumentCapability.Default;
            Options.DocumentCapabilities.Comments = DocumentCapability.Default;
            Options.DocumentCapabilities.EndNotes = DocumentCapability.Default;
            Options.DocumentCapabilities.Fields = DocumentCapability.Default;
            Options.DocumentCapabilities.FloatingObjects = DocumentCapability.Default;
            Options.DocumentCapabilities.FootNotes = DocumentCapability.Default;
            Options.DocumentCapabilities.ParagraphTabs = DocumentCapability.Default;
            DocumentModel.LayoutUnit = DevExpress.Office.DocumentLayoutUnit.Pixel;
        }
    }
}
