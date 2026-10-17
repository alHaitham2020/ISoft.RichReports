using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    /// <summary>
    /// Options for IF fields
    /// </summary>
    public class RichIfFieldOptions : RichFieldOptionsBase
    {
        public override RichFieldType FieldType => RichFieldType.IfField;

        [RichFieldArgument(0)]
        public string Expression1 { get; set; }

        [RichFieldArgument(1)]
        public string CmpOperator { get; set; }

        [RichFieldArgument(2)]
        public string Expression2 { get; set; }

        public static RichIfFieldOptions Create(string expression1, string cmOperator, string expression2)
        {
            return new RichIfFieldOptions
            {
                Expression1 = expression1,
                CmpOperator = cmOperator,
                Expression2 = expression2
            };
        }
    }
}
