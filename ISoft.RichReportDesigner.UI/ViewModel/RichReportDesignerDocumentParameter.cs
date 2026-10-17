using ISoft.RichReportDesigner.Core.Templates;
using ISoft.RichReportDesigner.UI.DataModel;
using ISoft.Utils.DataModel;

namespace ISoft.RichReportDesigner.UI.ViewModel
{
    /// <summary>
    /// Parameter object for designer document creation
    /// </summary>
    public class XpfRichReportDesignerDocumentParameter : ObservableObject
    {
        public IXpfRichReport Report { get; }

        public IRichReportTemplate Template { get; }

        public XpfRichReportDesignerDocumentParameter(IXpfRichReport report, IRichReportTemplate template)
        {
            Template = template;
            Report = report;
        }
    }
}
