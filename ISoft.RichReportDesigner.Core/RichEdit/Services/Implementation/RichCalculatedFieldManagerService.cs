using ISoft.RichReportDesigner.Core.Reports;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation
{
    public class RichCalculatedFieldManagerService : IRichCalculatedFieldManagerService
    {
        private readonly IRichReport _report;

        public RichCalculatedFieldManagerService(IRichReport report)
        {
            _report = report;
        }

        public IRichCalculatedFieldProperties CalculatedFieldProperties => _report.CalculatedFieldProperties;

        public string GetFieldDisplayName(string fieldName)
        {
            return _report.CalculatedFieldProperties.FindByName(fieldName)?.DisplayName ?? string.Empty;
        }

        public string GetFieldExpression(string fieldName)
        {
            return _report.CalculatedFieldProperties.FindByName(fieldName)?.Expression ?? null;
        }

        public bool TryGetFieldExpression(string fieldName, out string expression)
        {
            var result = _report.CalculatedFieldProperties.TryGet(fieldName, out var fieldProperty);
            expression = fieldProperty?.Expression ?? null;

            return result && expression != null;
        }
    }
}
