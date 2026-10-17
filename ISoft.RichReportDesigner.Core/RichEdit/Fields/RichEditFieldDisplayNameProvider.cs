using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Localization;

namespace ISoft.RichReportDesigner.Core.RichEdit.Fields
{
    public interface IRichEditFieldDisplayNameProvider
    {
        string GetDisplayName(RichFieldType fieldType);
    }

    public class RichEditFieldDisplayNameProvider : IRichEditFieldDisplayNameProvider
    {
        public string GetDisplayName(RichFieldType fieldType)
        {
            return RichFieldTypeNamesLocalizer.Get(fieldType);
        }
    }
}
