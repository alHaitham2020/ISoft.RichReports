using ISoft.RichReportDesigner.Core.Fields.Core;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public interface IRichDataField : IRichFieldNode
    {
        new IRichDataFieldGroup ParentGroup { get; }

        bool AllowUseDefaultEmptyValue { get; }

        string FieldDataName { get; }
    }

}
