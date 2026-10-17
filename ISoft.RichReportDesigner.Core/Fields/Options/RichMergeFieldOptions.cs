using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public class RichMergeFieldOptions : RichBarcodeFieldOptionsBase
    {
        public override RichFieldType FieldType => RichFieldType.MergeField;

        [RichFieldArgument(0)]
        public string DataFieldPath { get; set; }

        [RichFieldSwitch(RichFieldSwitchKeys.MailMergeField.Before)]
        public string StringBefore { get; set; }

        [RichFieldSwitch(RichFieldSwitchKeys.MailMergeField.After)]
        public string StringAfter { get; set; }

        public static RichMergeFieldOptions Create(string dataFieldPath, RichFieldFormat? format = null)
        {
            return new RichMergeFieldOptions() 
            {
                DataFieldPath = dataFieldPath ,
                Format = format
            };
        }
    }
}
