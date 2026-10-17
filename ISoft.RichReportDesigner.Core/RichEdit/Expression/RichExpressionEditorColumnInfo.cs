using DevExpress.Data.Controls.ExpressionEditor;

namespace ISoft.RichReportDesigner.Core.RichEdit.Expression
{
    public class RichExpressionEditorColumnInfo : ColumnInfo
    {
        public RichExpressionEditorColumnInfo()
        {

        }

        public RichExpressionEditorColumnInfo(string category) : base(category)
        {
        }

        public RichExpressionEditorColumnType ColumnType { get; set; }

        public string DataMember { get; set; }

        public bool IsDetailColumn {  get; set; }

        public bool HasValidDataMember => IsDetailColumn && !string.IsNullOrWhiteSpace(DataMember);

        public bool EffectDataMemberConflict
        {
            get
            {
                // calculated field column only EffectDataMemberConflict when HasValidDataMember 
                if (ColumnType == RichExpressionEditorColumnType.CalculatedColumn)
                {
                    return HasValidDataMember;
                }
                
                return true;
            }
        }
    }
}
