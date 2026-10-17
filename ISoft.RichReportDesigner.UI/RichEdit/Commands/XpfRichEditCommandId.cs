using DevExpress.XtraRichEdit.Commands;

namespace ISoft.RichReportDesigner.UI.RichEdit.Commands
{
    public class XpfRichEditCommandId
    {
        public static readonly RichEditCommandId ToolsFieldsCommandGroup = new(5000);

        public static readonly RichEditCommandId RemoveSelectedField = new(5001);

        public static readonly RichEditCommandId ChangeFieldBarcodeType = new(5002);

        public static readonly RichEditCommandId ChangeFieldImageWidth = new(5003);

        public static readonly RichEditCommandId ChangeFieldImageHeight = new(5004);
        
        public static readonly RichEditCommandId ChangeFieldDateTimeFormat = new(5005);

        public static readonly RichEditCommandId ChangeFieldNumericFormat = new(5006);

        public static readonly RichEditCommandId RichShowCalculatedFieldsEditorForm = new(5007);

        public static readonly RichEditCommandId RemoveSelectedFields = new(5008);


    }
}
