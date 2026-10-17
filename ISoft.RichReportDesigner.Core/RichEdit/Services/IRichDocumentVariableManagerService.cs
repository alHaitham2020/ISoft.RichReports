using DevExpress.XtraRichEdit;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichDocumentVariableManagerService
    {
        string GetDisplayName(string variableName);

        void Calculate(CalculateDocumentVariableEventArgs e);
    }
}
