using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    /// <summary>
    /// Common Options Can be Shared for any fields
    /// </summary>
    public class RichCommonFieldsOptions : RichFormatableFieldOptions
    {
        [RichFieldArgument(0)]
        public string FirstArgument { get; set; }

        public RichCommonFieldsOptions(RichEditFieldType fieldType, string firstArgument, RichFieldFormat? format = null) : base(fieldType, format)
        {
            FirstArgument = firstArgument;
        }

        public static RichCommonFieldsOptions Create(RichEditFieldType fieldType, string firstArgument, RichFieldFormat? format = null)
        {
            return new RichCommonFieldsOptions(fieldType, firstArgument, format);
        }
    }
}
