using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Model;
using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.RichEdit.Services;

namespace ISoft.RichReportDesigner.Core.RichEdit.Fields
{
    public class RichPrintDateField : PrintDateField, IRichEditField
    {
        private string _displayName;

        public new static RichPrintDateField Create()
        {
            return new RichPrintDateField();
        }

        protected override FieldMailMergeType MailMergeType()
        {
            return FieldMailMergeType.MailMerge;
        }

        protected override string GetDefaultMailMergeFieldText()
        {
            return this.CalculateDisplayName(_displayName, FieldType);
        }

        public override void Initialize(PieceTable pieceTable, InstructionCollection switches)
        {
            base.Initialize(pieceTable, switches);

            IRichEditFieldsManagerService service = pieceTable.DocumentModel.GetService<IRichEditFieldsManagerService>();
            _displayName = service?.GetFieldDisplayName(RichFieldType.PrintDateField);
        }
    }
}
