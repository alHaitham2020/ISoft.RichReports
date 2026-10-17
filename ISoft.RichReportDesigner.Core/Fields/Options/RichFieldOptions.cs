using ISoft.RichReportDesigner.Core.Fields.Core;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    /// <summary>
    /// empty options with none arguments and none switches
    /// </summary>
    public class RichFieldOptions : RichFieldOptionsBase
    {
        private readonly RichFieldType _fieldType;

        public override RichFieldType FieldType => _fieldType;

        public RichFieldOptions(RichFieldType fieldType)
        {
            _fieldType = fieldType;
        }

        public static RichFieldOptions Create(RichFieldType fieldType)
        {
            return new RichFieldOptions(fieldType);
        }
    }
}
