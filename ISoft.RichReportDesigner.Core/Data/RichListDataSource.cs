using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Data
{
    public class RichListDataSource : RichDataSourceBase
    {
        private readonly IRichDataFieldsBuilder _builder;

        public RichListDataSource(Type itemType, IList data)
            : this(itemType, data, new RichDataFieldsBuildOptions())
        {

        }

        public RichListDataSource(Type itemType, IList data, IRichDataFieldsBuildOptions options) 
            : this(itemType, data, new RichDataFieldsReflectionBuilder(options))
        {

        }

        public RichListDataSource(Type itemType, IList data, IRichDataFieldsBuilder builder) 
            : base(itemType, data)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public override RichDataFieldGroup[] GetDataFields()
        {
            return _builder.BuildFromType(SourceType);
        }

        public override async Task<RichDataFieldGroup[]> GetDataFieldsAsync()
        {
            return await Task.Run(GetDataFields);
        }
    }
}
