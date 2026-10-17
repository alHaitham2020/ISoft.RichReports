using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;

namespace ISoft.RichReportDesigner.Core.Fields.Options
{
    public abstract class RichFormatableFieldOptionsBase : RichFieldOptionsBase
    {
        [RichFieldFormatSwitch]
        public RichFieldFormat? Format { get; set; }

        [RichFieldSwitch(RichFieldSwitchKeys.GeneralFormat.MERGEFORMAT)]
        public bool PreserveFormatting { get; set; }
    }
}
