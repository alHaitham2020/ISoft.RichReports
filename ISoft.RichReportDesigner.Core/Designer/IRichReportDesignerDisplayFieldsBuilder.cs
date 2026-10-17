using ISoft.RichReportDesigner.Core.Fields.Display;
using ISoft.RichReportDesigner.Core.Reports;

namespace ISoft.RichReportDesigner.Core.Designer
{
    public interface IRichReportDesignerDisplayFieldsBuilder
    {
        IRichDisplayFieldGroup[] Build(IRichReport report);
    }
}
