using ISoft.RichReportDesigner.Core.Fields.Format;

namespace ISoft.RichReportDesigner.Core.Fields.DataAnnotations
{
    public class RichDateTimeDataFieldFormatAttribute : RichDataFieldFormatAttributeBase
    {
        private readonly RichFieldDateTimeFormatType _formatType;

        public RichDateTimeDataFieldFormatAttribute(RichFieldDateTimeFormatType formatType)
        {
            _formatType = formatType;
        }

        public override RichFieldFormat? GetFormat()
        {
            return RichFieldFormatHelper.GetDateTimeFormat(_formatType);
        }
    }
}
