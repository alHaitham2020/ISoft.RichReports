using ISoft.RichReportDesigner.Core.Fields.Core;
using System.Drawing;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayFieldGroup : IRichFieldGroup
    {
        Color BackgroundColor { get; }

        Color FontColor { get; }

        new IRichDisplayFieldGroup ParentGroup { get; }

        IRichDisplayFieldsCollection Fields { get; }
    }
}
