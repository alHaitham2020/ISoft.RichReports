using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Data
{
    public abstract class RichDataSourceBase : IRichDataSource
    {
        private readonly Type _sourceType;
        private readonly object _data;

        public Type SourceType => _sourceType;

        public object Data => _data;

        protected RichDataSourceBase(Type sourceType, object data)
        {
            _sourceType = sourceType;
            _data = data;
        }

        public abstract RichDataFieldGroup[] GetDataFields();

        public abstract Task<RichDataFieldGroup[]> GetDataFieldsAsync();
    }
}
