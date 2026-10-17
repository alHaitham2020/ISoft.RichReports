using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichDocVariableFieldOptions : RichFormatableFieldOptionsBase
    {
        public override RichFieldType FieldType => RichFieldType.DocVariableField;

        [RichFieldArgument(0)]
        public string VariableName { get; set; }

        public static RichDocVariableFieldOptions Create(string variableName, RichFieldFormat? format = null)
        {
            return new RichDocVariableFieldOptions()
            {
                VariableName = variableName,
                Format = format
            };
        }
    }
}
