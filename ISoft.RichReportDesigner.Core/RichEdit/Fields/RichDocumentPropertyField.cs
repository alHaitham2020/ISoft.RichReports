using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Model;
using ISoft.RichReportDesigner.Core.RichEdit.Services;

namespace ISoft.RichReportDesigner.Core.RichEdit.Fields
{
    public class RichDocumentPropertyField : DocumentPropertyField, IRichEditField
    {
        private string _displayName;

        public new static CalculatedFieldBase Create()
        {
            return new RichDocumentPropertyField();
        }

        protected override FieldMailMergeType MailMergeType()
        {
            return FieldMailMergeType.MailMerge;
        }

        protected override string GetDefaultMailMergeFieldText()
        {
            string fieldDisplayName = this.CalculateDisplayName(_displayName, PropertyName);

            if (string.IsNullOrWhiteSpace(fieldDisplayName))
            {
                return GetDefaultInvalidValueText();
            }

            return fieldDisplayName;
        }

        public override void Initialize(PieceTable pieceTable, InstructionCollection switches)
        {
            base.Initialize(pieceTable, switches);

            IRichDocumentPropertiesManagerService service = pieceTable.DocumentModel.GetService<IRichDocumentPropertiesManagerService>();
            _displayName = service?.GetDisplayName(PropertyName);
        }
    }
}
