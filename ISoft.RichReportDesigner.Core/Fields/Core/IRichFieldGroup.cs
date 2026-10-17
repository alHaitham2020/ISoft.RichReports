using ISoft.RichReportDesigner.Core.Fields.Display;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    public interface IRichFieldGroup : IRichNode
    {
        IRichFieldGroup ParentGroup { get;}

        RichDisplayFieldGroupCategory Category { get;}
    }
}
