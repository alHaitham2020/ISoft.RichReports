using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public interface IRichDataFieldCollection : IRichDataFieldReadOnlyCollection
    {
        internal void LoadFrom(IEnumerable<IRichDataField> source);
    }

}
