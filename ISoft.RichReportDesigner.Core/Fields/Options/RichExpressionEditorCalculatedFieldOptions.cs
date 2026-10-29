using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichExpressionEditorCalculatedFieldOptions : RichBarcodeFieldOptionsBase
    {
        public override RichEditFieldType FieldType => RichEditFieldType.ExpEditCalculatedField;

        [RichFieldArgument(0)]
        public string ExpressionString { get; set; }

        public static RichExpressionEditorCalculatedFieldOptions Create(string expressionString, RichFieldFormat? format = null)
        {
            return new RichExpressionEditorCalculatedFieldOptions()
            {
                ExpressionString = expressionString,
                Format = format
            };
        }
    }
}
