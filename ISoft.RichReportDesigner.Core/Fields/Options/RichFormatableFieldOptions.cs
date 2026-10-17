using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichFormatableFieldOptions : RichFormatableFieldOptionsBase
    {
        private readonly RichFieldType _fieldType;

        public override RichFieldType FieldType => _fieldType;

        public RichFormatableFieldOptions(RichFieldType fieldType, RichFieldFormat? format = null)
        {
            _fieldType = fieldType;
            Format = format;
        }

        public static RichFormatableFieldOptions Create(RichFieldType fieldType,RichFieldFormat? format = null)
        {
            return new RichFormatableFieldOptions(fieldType, format);
        }
    }
}
