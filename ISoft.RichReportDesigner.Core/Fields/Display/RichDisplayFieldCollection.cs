using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.Utils.Data.Collection;
using ISoft.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public class RichDisplayFieldCollection : ObservableUniqueDataCollectionBase<RichFieldKey, IRichDisplayField>, IRichDisplayFieldsCollection
    {
        private static readonly Func<IRichDisplayField, RichFieldKey> _selector = (x) => x.FieldKey;

        public RichDisplayFieldCollection() : base(_selector)
        {

        }

        public RichDisplayFieldCollection(IEnumerable<IRichDisplayField> collection) : base(collection, _selector)
        {
            
        }

        protected override bool IsValidKey(RichFieldKey key) => key.IsValid();

        protected override IRichDisplayField CloneItem(IRichDisplayField item) => item.CreateClone();

        IEnumerable<RichFieldKey> IRichDisplayFieldsReadOnlyCollection.FieldKeys => Keys;

        IRichDisplayField IRichDisplayFieldsReadOnlyCollection.FindByFieldProperty(string fieldProperty)
        {
            return Values.SingleOrDefault(x => fieldProperty.EqualsTo(x.FieldProperty));
        }

        bool IRichDisplayFieldsReadOnlyCollection.ContainsField(RichFieldKey key) => ContainsKey(key);

        IRichDisplayField IRichDisplayFieldsReadOnlyCollection.GetFieldByKey(RichFieldKey key) => GetItem(key);

        bool IRichDisplayFieldsReadOnlyCollection.TryGetField(RichFieldKey key, out IRichDisplayField item) => TryGetItem(key, out item);

        void IRichDisplayFieldsCollection.LoadFrom(IEnumerable<IRichDisplayField> source) => LoadFrom(source);
    }
}
