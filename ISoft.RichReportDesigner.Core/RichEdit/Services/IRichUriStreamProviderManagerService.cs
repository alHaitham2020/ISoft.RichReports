using DevExpress.Office.Services;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichUriStreamProviderManagerService
    {
        void RegisterUriStreamProviders(IUriStreamService uriStreamService);

        void UnRegisterUriStreamProviders(IUriStreamService uriStreamService);

    }
}
