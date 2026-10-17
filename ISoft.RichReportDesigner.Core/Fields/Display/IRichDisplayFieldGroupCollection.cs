using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayFieldGroupCollection : IRichDisplayFieldGroupReadOnlyCollection
    {
        internal void LoadFromSource(IEnumerable<IRichDisplayFieldGroup> source);

        internal void Clear();

        internal void InvalidateCache();
    }
}
