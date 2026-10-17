
using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.Reports;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;
using System.Linq;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation
{
    public class RichDefaultDocumentCustomPropertiesManagerService : IRichDocumentCustomPropertiesManagerService
    {
        private readonly IRichReport _report;

        public RichDefaultDocumentCustomPropertiesManagerService(IRichReport report)
        {
            _report = report;
        }

        public string GetDisplayName(string propertyName)
        {
            return _report
                     .DocumentCustomProperties
                     .GetProperty(propertyName)?.DisplayName ?? string.Empty;
        }

        public virtual void UpdateDocCustomProperties(DocumentCustomProperties docProperties)
        {
            var customProperties = _report.DocumentCustomProperties.ToList();
            foreach(var property in customProperties)
            {
                docProperties[property.Name] = property.GetDisplayValue();
            }
        }
    }
}
