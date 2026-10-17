using ISoft.RichReportDesigner.Core.Fields.Core;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public interface IRichDataFieldGroup : IRichFieldGroup
    {
        new IRichDataFieldGroup ParentGroup { get; }

        IRichDataFieldCollection Fields { get; }
    }
}
