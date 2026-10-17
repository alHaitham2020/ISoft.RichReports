using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    /// <summary>
    /// Options for Date based fields
    /// </summary>
    public class RichDateBasedFieldOptions : RichFormatableFieldOptionsBase
    {
        private readonly RichFieldType _fieldType;

        public override RichFieldType FieldType => _fieldType;

        [RichFieldSwitch(RichFieldSwitchKeys.DateField.UseLunarCalendar)]
        public bool UseLunarCalendar { get; set; }

        [RichFieldSwitch(RichFieldSwitchKeys.DateField.UseSakaEraCalendar)]
        public bool UseSakaEraCalendar { get; set; }

        [RichFieldSwitch(RichFieldSwitchKeys.DateField.UseLastFormat)]
        public bool UseLastFormat { get; set; }

        public RichDateBasedFieldOptions(RichFieldType fieldType, RichFieldFormat? format = null)
        {
            _fieldType = fieldType;
            Format = format;
        }

        public static RichDateBasedFieldOptions Create(RichFieldType fieldType, RichFieldFormat? format = null)
        {
            return new RichDateBasedFieldOptions(fieldType, format);
        }
    }
}
