using ISoft.Utils.Localization;
using System.Resources;

namespace ISoft.RichReportDesigner.Core.Localization
{
    public class RichReportResourceLocalizer : ResourceLocalizerBase<RichReportStringId>
    {
        public const string ResourceFilePath = "ISoft.RichReportDesigner.Core.Localization.Resources.ReportResources";

        public RichReportResourceLocalizer()
            : base(new RichReportLocalizer())
        {
            
        }

        protected override ResourceManager CreateResourceManagerCore()
        {
            return new ResourceManager(ResourceFilePath, typeof(RichReportResourceLocalizer).Assembly);
        } 
    }
}
