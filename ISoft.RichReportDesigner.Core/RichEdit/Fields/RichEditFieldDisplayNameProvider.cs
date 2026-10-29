using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Localization;

namespace ISoft.RichReportDesigner.Core.RichEdit.Fields
{
    public interface IRichEditFieldDisplayNameProvider
    {
        string GetDisplayName(RichEditFieldType fieldType);
    }

    public class RichEditFieldDisplayNameProvider : IRichEditFieldDisplayNameProvider
    {
        public string GetDisplayName(RichEditFieldType fieldType)
        {
            return RichFieldTypeNamesLocalizer.Get(fieldType);
        }
    }
}
