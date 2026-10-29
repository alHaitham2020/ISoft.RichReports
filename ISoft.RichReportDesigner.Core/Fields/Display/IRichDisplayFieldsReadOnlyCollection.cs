using ISoft.RichReportDesigner.Core.Fields.Core;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayFieldsReadOnlyCollection : IReadOnlyCollection<IRichDisplayField>
    {
        IRichDisplayField this[RichEditFieldKey key] { get; }

        IEnumerable<RichEditFieldKey> FieldKeys { get; }

        bool ContainsField(RichEditFieldKey key);
        IRichDisplayField FindByFieldProperty(string fieldProperty);
        IRichDisplayField GetFieldByKey(RichEditFieldKey key);

        bool TryGetField(RichEditFieldKey key, out IRichDisplayField item);
    }

}
