
using ISoft.RichReportDesigner.Core.Fields.Core;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichNestedFieldOptions : RichFieldOptionsBase
    {
        public RichFieldOptionsBase OuterFieldOptions { get; set; }

        public RichFieldOptionsBase InnerFieldOptions { get; set; }

        public override RichFieldType FieldType => throw new System.NotImplementedException();

        public static RichNestedFieldOptions CreateIncludePictureWithInnerOptions(RichFieldOptionsBase innerFieldsOptions)
        {
            return CreateIncludePictureWithInnerOptions(RichIncludePictureFieldOptions.CreateDefault(), innerFieldsOptions);
        }

        public static RichNestedFieldOptions CreateIncludePictureWithInnerOptions(
            RichIncludePictureFieldOptions includePictureOptions,
            RichFieldOptionsBase innerFieldsOptions)
        {
            var nestedFieldOptions = new RichNestedFieldOptions
            {
                OuterFieldOptions = includePictureOptions,
                InnerFieldOptions = innerFieldsOptions
            };

            return nestedFieldOptions;
        }
    }
}
