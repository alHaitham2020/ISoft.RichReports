using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichDocCustomPropertyFieldOptions : RichFormatableFieldOptionsBase
    {
        public override RichEditFieldType FieldType => RichEditFieldType.CustomPropertyField;

        [RichFieldArgument(0)]
        public string PropertyName { get; set; }

        public static RichDocCustomPropertyFieldOptions Create(string propertyName, RichFieldFormat? format = null)
        {
            return new RichDocCustomPropertyFieldOptions()
            {
                PropertyName = propertyName,
                Format = format
            };
        }
    }
}
