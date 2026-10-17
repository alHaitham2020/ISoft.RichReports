using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public interface IRichDataFieldGroupCollection : IReadOnlyCollection<IRichDataFieldGroup>
    {
        IEnumerable<string> GroupPaths { get; }

        bool HasDataGroups { get; }

        bool ContainsGroup(string path);

        bool TryGetGroup(string path, out IRichDataFieldGroup item);

        IRichDataField FindFieldByPath(string path);

        IRichDataFieldGroup FindGroup(string path);

        IRichDataFieldGroup FindGroupContainingField(string fieldPath);

        internal void LoadFromSource(IEnumerable<IRichDataFieldGroup> source);
    }
}
