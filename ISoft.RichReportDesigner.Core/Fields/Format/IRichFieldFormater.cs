using ISoft.RichReportDesigner.Core.Fields.Codes;
using ISoft.RichReportDesigner.Core.Fields.Switches;

namespace ISoft.RichReportDesigner.Core.Fields.Format
{
    public interface IRichFieldFormater
    {
        void Insert(IRichFieldCode fieldCode, IRichFieldSwitchBuilder switchBuilder);
    }
}
