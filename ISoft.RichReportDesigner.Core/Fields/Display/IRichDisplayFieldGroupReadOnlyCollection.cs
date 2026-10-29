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

        IRichDisplayField FindFieldByKey(RichEditFieldKey fieldKey);

        IRichDisplayField FindNoneDataFieldByKey(RichEditFieldKey fieldKey);

        IRichDisplayFieldGroup FindGroup(string path);

        IRichDisplayField FindDataFieldByPath(string fieldPath);

        IRichDisplayFieldGroup FindGroupContainingField(RichEditFieldKey fieldKey);

        IRichDisplayField FindDataFieldByKey(RichEditFieldKey fieldKey);

        IRichDisplayFieldGroup FindGroupByCategory(RichDisplayFieldGroupCategory category);

        IRichDisplayFieldGroupReadOnlyCollection GetGroupsIn(params RichDisplayFieldGroupCategory[] categories);

        IRichDisplayFieldGroupReadOnlyCollection GetGroupsNotIn(params RichDisplayFieldGroupCategory[] categories);

        IReadOnlyCollection<IRichDisplayField> GetAllFields();

        IRichDisplayFieldGroupReadOnlyCollection GetNoneDataFieldGroups();

        IRichDisplayFieldGroupReadOnlyCollection GetDataFieldGroups();

        IReadOnlyCollection<IRichDisplayField> GetNoneDataFields();

    }
}
