using ISoft.RichReportDesigner.Core.Fields.Core;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayFieldsReadOnlyCollection : IReadOnlyCollection<IRichDisplayField>
    {
        IRichDisplayField this[RichFieldKey key] { get; }

        IEnumerable<RichFieldKey> FieldKeys { get; }

        bool ContainsField(RichFieldKey key);
        IRichDisplayField FindByFieldProperty(string fieldProperty);
        IRichDisplayField GetFieldByKey(RichFieldKey key);

        bool TryGetField(RichFieldKey key, out IRichDisplayField item);
    }

}
