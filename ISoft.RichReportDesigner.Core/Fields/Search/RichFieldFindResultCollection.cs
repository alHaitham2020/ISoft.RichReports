using ISoft.Utils.Data.Collection;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Search
{
    public interface IRichFieldFindResultCollection : IReadOnlyDataCollection<IRichFieldFindResult>
    {

    }

    public class RichFieldFindResultCollection : List<IRichFieldFindResult>, IRichFieldFindResultCollection
    {
        public RichFieldFindResultCollection()
        {
            
        }

        public RichFieldFindResultCollection(IEnumerable<IRichFieldFindResult> collection) : base(collection)
        {

        }

        public RichFieldFindResultCollection(int capacity) : base(capacity)
        {
        }
    }
}
