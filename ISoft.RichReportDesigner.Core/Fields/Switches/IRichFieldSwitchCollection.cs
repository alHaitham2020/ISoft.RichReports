using ISoft.Utils.Caching;

namespace ISoft.RichReportDesigner.Core.Fields.Switches
{
    public interface IRichFieldSwitchCollection : IRichFieldSwitchReadOnlyCollection, IUniqueItemsCache<string, IRichFieldSwitch>
    {
        void AddOrUpdateSwitch(string switchCode, string switchValue);

        bool TryUpdateSwitch(string switchCode, string switchValue);

        void RemoveTypeSwitches(RichSwitchType switchType);
    }
}
