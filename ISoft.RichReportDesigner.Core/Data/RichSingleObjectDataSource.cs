using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.Collections;

namespace ISoft.RichReportDesigner.Core.Data
{
    public sealed class RichSingleObjectDataSource : RichListDataSource
    {
        public RichSingleObjectDataSource(Type itemType, object data) : base(itemType, new ArrayList(1) { data })
        {

        }

        public RichSingleObjectDataSource(Type itemType, object data, IRichDataFieldsBuildOptions options) : base(itemType, new ArrayList(1) { data }, options)
        {
        }

        public RichSingleObjectDataSource(Type itemType, object data, IRichDataFieldsBuilder builder) : base(itemType, new ArrayList(1) { data }, builder)
        {
        }

        public static RichSingleObjectDataSource Create<T>(T data)
        {
            return new RichSingleObjectDataSource(typeof(T), data);
        }

        public static RichSingleObjectDataSource Create<T>(T data, IRichDataFieldsBuildOptions options)
        {
            return new RichSingleObjectDataSource(typeof(T), data, options);
        }
    }
}
