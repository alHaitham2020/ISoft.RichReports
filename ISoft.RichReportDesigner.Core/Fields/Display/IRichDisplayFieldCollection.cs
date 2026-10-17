using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayFieldsCollection : IRichDisplayFieldsReadOnlyCollection
    {
        internal void LoadFrom(IEnumerable<IRichDisplayField> source);
    }

}
