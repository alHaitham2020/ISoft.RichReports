using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.Utils.Localization;

namespace ISoft.RichReportDesigner.Core.Localization
{
    public class RichFieldTypeNamesLocalizer : ResourceManagerLocalizerBase<RichEditFieldType>
    {
        public const string ResourceFilePath = "ISoft.RichReportDesigner.Core.Localization.Resources.RichFieldTypeNameResources";

        protected override string ResourceBaseName => ResourceFilePath;

        public static readonly IResourceManagerLocalizer<RichEditFieldType> Current = new RichFieldTypeNamesLocalizer();

        public RichFieldTypeNamesLocalizer()
        {
            
        }

        public static string Get(RichEditFieldType id)
        {
            return Current.GetLocalizedString(id);
        }
    }
}
