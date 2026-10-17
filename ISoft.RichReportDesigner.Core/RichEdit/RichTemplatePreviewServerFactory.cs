namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public class RichTemplatePreviewServerFactory : IRichTemplatePreviewServerFactory
    {
        private readonly IRichServiceContainer _serviceContainer;

        public RichTemplatePreviewServerFactory(IRichServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public IRichDocumentPreviewServer Create()
        {
            return RichDocumentPreviewServer.Create(_serviceContainer);
        }
    }
}
