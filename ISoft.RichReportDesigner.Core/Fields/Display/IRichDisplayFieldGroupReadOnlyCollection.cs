using ISoft.RichReportDesigner.Core.Fields.Core;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayFieldGroupReadOnlyCollection : IReadOnlyCollection<IRichDisplayFieldGroup>
    {
        IEnumerable<string> GroupPaths { get; }

        bool ContainsGroup(string path);

        bool TryGetGroup(string path, out IRichDisplayFieldGroup item);

        IRichDisplayField FindFieldByPath(string path);

        IRichDisplayField FindFieldByKey(RichFieldKey fieldKey);

        IRichDisplayField FindNoneDataFieldByKey(RichFieldKey fieldKey);

        IRichDisplayFieldGroup FindGroup(string path);

        IRichDisplayField FindDataFieldByPath(string fieldPath);

        IRichDisplayFieldGroup FindGroupContainingField(RichFieldKey fieldKey);

        IRichDisplayField FindDataFieldByKey(RichFieldKey fieldKey);

        IRichDisplayFieldGroup FindGroupByCategory(RichDisplayFieldGroupCategory category);

        IRichDisplayFieldGroupReadOnlyCollection GetGroupsIn(params RichDisplayFieldGroupCategory[] categories);

        IRichDisplayFieldGroupReadOnlyCollection GetGroupsNotIn(params RichDisplayFieldGroupCategory[] categories);

        IReadOnlyCollection<IRichDisplayField> GetAllFields();

        IRichDisplayFieldGroupReadOnlyCollection GetNoneDataFieldGroups();

        IRichDisplayFieldGroupReadOnlyCollection GetDataFieldGroups();

        IReadOnlyCollection<IRichDisplayField> GetNoneDataFields();

    }
}
