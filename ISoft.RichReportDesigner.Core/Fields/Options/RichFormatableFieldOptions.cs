using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichFormatableFieldOptions : RichFormatableFieldOptionsBase
    {
        private readonly RichEditFieldType _fieldType;

        public override RichEditFieldType FieldType => _fieldType;

        public RichFormatableFieldOptions(RichEditFieldType fieldType, RichFieldFormat? format = null)
        {
            _fieldType = fieldType;
            Format = format;
        }

        public static RichFormatableFieldOptions Create(RichEditFieldType fieldType,RichFieldFormat? format = null)
        {
            return new RichFormatableFieldOptions(fieldType, format);
        }
    }
}
