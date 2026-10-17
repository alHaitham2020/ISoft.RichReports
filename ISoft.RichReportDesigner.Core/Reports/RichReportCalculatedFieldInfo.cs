using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.Utils.DataModel;
using System;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportCalculatedFieldInfo
    {
        Guid? Id { get; }

        string FieldName { get; }

        int? ReportId { get; }

        string DataMember { get; }

        string DisplayName { get; }

        string Expression { get; }

        RichCalculatedFieldType? FieldType { get;}
    }

    public class RichReportCalculatedFieldInfo : ObjectBase, IRichReportCalculatedFieldInfo
    {
        public Guid? Id { get; set; }

        public int? ReportId { get; set; }

        public string FieldName { get; set; }

        public string DisplayName { get; set; }

        public string DataMember { get; set; }

        public string Expression { get; set; }

        public RichCalculatedFieldType? FieldType { get; set; }
    }
}
