using ISoft.Utils.Caching;
using System.Text;

namespace ISoft.RichReportDesigner.Core.Fields.Switches
{
    public interface IRichFieldSwitchReadOnlyCollection : IUniqueReadOnlyItemsCache<string, IRichFieldSwitch>
    {
        string GetSwitchValue(string switchCode);

        T GetSwitchValue<T>(string switchCode, T defaultValue = default);

        string GetBuildString();

        void BuildTo(StringBuilder sb);
    }
}
