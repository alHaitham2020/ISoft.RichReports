using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichDocPropertyFieldOptions : RichFormatableFieldOptionsBase
    {
        public override RichEditFieldType FieldType => RichEditFieldType.DocPropertyField;

        [RichFieldArgument(0)]
        public string PropertyName { get; set; }

        public static RichDocPropertyFieldOptions Create(string propertyName, RichFieldFormat? format = null)
        {
            return new RichDocPropertyFieldOptions()
            {
                PropertyName = propertyName,
                Format = format
            };
        }
    }
}
