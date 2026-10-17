using ISoft.RichReportDesigner.Core.Fields.Argumentes;
using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Switches;

namespace ISoft.RichReportDesigner.Core.Fields.Codes
{
    public interface IRichFieldCode
    {
        RichFieldInfo FieldInfo { get; }

        RichFieldType FieldType { get;}

        string FieldName { get; }

        string FieldProperty { get; }

        RichFieldKey FieldKey { get; }

        bool IsTableStart { get; }

        bool IsTableEnd { get; }

        bool IsRegion { get; }

        bool IsDataTableField { get;}

        string OriginalCode { get;}

        IRichFieldArgumentCollection Arguments { get; }

        IRichFieldSwitchCollection Switches { get; }

        string GetDataTableName();

        string Build();

        bool TryBuild(out string result);

        bool IsValid();

        IRichFieldCode Copy();
        void Validate();
    }
}
