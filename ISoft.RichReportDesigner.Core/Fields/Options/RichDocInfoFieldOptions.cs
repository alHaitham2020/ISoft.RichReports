using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichDocInfoFieldOptions : RichFormatableFieldOptionsBase
    {
        public override RichEditFieldType FieldType => RichEditFieldType.InfoField;

        [RichFieldArgument(0)]
        public string PropertyName { get; set; }

        public static RichDocInfoFieldOptions Create(string propertyName, RichFieldFormat? format = null)
        {
            return new RichDocInfoFieldOptions()
            {
                PropertyName = propertyName,
                Format = format
            };
        }
    }
}
