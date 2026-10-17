using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    public interface IRichCalculatedFieldProperties : IReadOnlyCollection<IRichCalculatedFieldProperty>
    {
        IEnumerable<string> Names { get; }

        bool HasProprties{ get; }

        bool Contains(string name);

        bool TryGet(string name, out IRichCalculatedFieldProperty item);

        IRichCalculatedFieldProperty FindByName(string name);

        internal void LoadFromSource(IEnumerable<IRichCalculatedFieldProperty> source);
    }
}
