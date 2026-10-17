using DevExpress.XtraRichEdit.Model;

namespace ISoft.RichReportDesigner.Core.RichEdit.Data
{
    public class RichEditDataController : RichDataController
    {
        public static RichEditDataController CreateDefault()
        {
            return new RichEditDataController()
            {
                AllowIEnumerableDetails = true,
                AllowDetailDescriptors = true,
            };
        }
    }
}
