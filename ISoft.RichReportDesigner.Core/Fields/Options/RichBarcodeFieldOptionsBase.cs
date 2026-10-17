using ISoft.Office.Barcode;
using ISoft.RichReportDesigner.Core.Fields.Instructions;
using ISoft.Utils.DataAnnotations;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public abstract class RichBarcodeFieldOptionsBase : RichImageFieldOptionsBase
    {
        [RichFieldSwitch(RichFieldSwitchKeys.MailMergeField.Barcode)]
        [DefaultNullValue(BarcodeType.None)]
        public BarcodeType BarcodeType { get; set; } = BarcodeType.None;
    }
}
