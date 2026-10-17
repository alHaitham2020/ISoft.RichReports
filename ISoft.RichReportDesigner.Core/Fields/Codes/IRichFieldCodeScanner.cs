using ISoft.RichReportDesigner.Core.Fields.Switches;

namespace ISoft.RichReportDesigner.Core.Fields.Codes
{
    public interface IRichFieldCodeScanner
    {
        IRichFieldCode Scan(string rawCodeText);

        IRichFieldCodeCollection ScanMultiple(string rawCodeText);

        IRichFieldSwitchReadOnlyCollection ScanSwitches(string parameters);

        IRichFieldSwitch FindSpecificSwitch(string parameters, string targetSwitchKey);

        RichFieldSwitchInfo GetSwitchInfo(string rawCodeText, string switchKey);
    }
}
