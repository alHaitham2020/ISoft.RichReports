using ISoft.RichReportDesigner.Core.Fields.Format;

namespace ISoft.RichReportDesigner.Core.Fields.DataAnnotations
{
    public class RichDataFieldGeneralFormatAttribute : RichDataFieldFormatAttributeBase
    {
        private readonly RichFieldGeneralFormatType _formatType;

        public RichDataFieldGeneralFormatAttribute(RichFieldGeneralFormatType formatType)
        {
            _formatType = formatType;
        }

        public override RichFieldFormat? GetFormat()
        {
            return RichFieldFormatHelper.GetGeneralFormat(_formatType);
        }
    }
}
