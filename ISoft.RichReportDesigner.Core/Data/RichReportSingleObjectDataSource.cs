using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.Collections;

namespace ISoft.RichReportDesigner.Core.Data
{
    public sealed class RichReportSingleObjectDataSource : RichReportListDataSource
    {
        public RichReportSingleObjectDataSource(Type itemType, object data) : base(itemType, new ArrayList(1) { data })
        {

        }

        public RichReportSingleObjectDataSource(Type itemType, object data, IRichDataFieldsBuildOptions options) : base(itemType, new ArrayList(1) { data }, options)
        {
        }

        public RichReportSingleObjectDataSource(Type itemType, object data, IRichDataFieldsBuilder builder) : base(itemType, new ArrayList(1) { data }, builder)
        {
        }

        public static RichReportSingleObjectDataSource Create<T>(T data)
        {
            return new RichReportSingleObjectDataSource(typeof(T), data);
        }

        public static RichReportSingleObjectDataSource Create<T>(T data, IRichDataFieldsBuildOptions options)
        {
            return new RichReportSingleObjectDataSource(typeof(T), data, options);
        }
    }
}
