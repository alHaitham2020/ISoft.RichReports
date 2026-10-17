using ISoft.RichReportDesigner.Core.RichEdit.Properties;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichCalculatedFieldManagerService
    {
        IRichCalculatedFieldProperties CalculatedFieldProperties { get; }

        string GetFieldDisplayName(string fieldName);

        string GetFieldExpression(string fieldName);

        bool TryGetFieldExpression(string fieldName, out string expression);
    }
}
