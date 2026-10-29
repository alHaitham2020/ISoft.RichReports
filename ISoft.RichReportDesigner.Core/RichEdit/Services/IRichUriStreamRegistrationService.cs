using DevExpress.Office.Services;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichUriStreamRegistrationService
    {
        void RegisterUriStreamProviders(IUriStreamService uriStreamService);

        void UnRegisterUriStreamProviders(IUriStreamService uriStreamService);

    }
}
