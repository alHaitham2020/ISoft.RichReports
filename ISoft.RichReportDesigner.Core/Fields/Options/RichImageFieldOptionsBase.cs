using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public abstract class RichImageFieldOptionsBase : RichFormatableFieldOptionsBase
    {
        [RichFieldSwitch(RichFieldSwitchKeys.ImageBasedField.ImageWidth)]
        public int? ImageWidth { get; set; }

        [RichFieldSwitch(RichFieldSwitchKeys.ImageBasedField.ImageHeight)]
        public int? ImageHeight { get; set; }
    }
}
