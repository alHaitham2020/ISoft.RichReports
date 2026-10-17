using DevExpress.XtraPrinting;
using ISoft.RichReportDesigner.Core.Printing;
using ISoft.RichReportDesigner.Core.RichEdit;
using ISoft.RichReportDesigner.Core.RichEdit.Services;
using ISoft.Utils.Helpers;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public class RichReportPrintManager : IRichPrintManager
    {
        private readonly IRichServiceContainer _serviceContainer;

        public RichReportPrintManager(IRichServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer ?? throw new ArgumentNullException(nameof(serviceContainer));
        }

        public async Task ShowPrintPreviewAsync(object templateSource, object dataSource, string title = null)
        {
            await ExecutePrintOperationAsync(
                templateSource,
                dataSource,
                async server => server.ShowPrintPreview(title),
                "جاري تجهيز المعاينة ..."
            );
        }

        public async Task ShowPrintDialogAsync(object templateSource, object dataSource)
        {
            await ExecutePrintOperationAsync(
                templateSource,
                dataSource,
                server => server.ShowPrintDialog(),
                "جاري تجهيز مربع حوار الطباعة ..."
            );
        }

        public async Task PrintAsync(object templateSource, object dataSource)
        {
            await ExecutePrintOperationAsync(
                templateSource,
                dataSource,
                server => server.Print(),
                "جاري الطباعة ..."
            );
        }

        public async Task ExportAsync(object templateSource, object dataSource, ExportFormat format, string defaultFileName = null)
        {
            await ExecutePrintOperationAsync(
                templateSource,
                dataSource,
                server => server.Export(format, defaultFileName),
                "جاري التصدير ..."
            );
        }

        private async Task ExecutePrintOperationAsync(
            object templateSource,
            object dataSource,
            Action<IRichDocumentPreviewServer> operation,
            string initialMessage)
        {
            ArgumentGuardHelper.ArgumentNotNull(templateSource, nameof(templateSource));
            ArgumentGuardHelper.ArgumentNotNull(dataSource, nameof(dataSource));

            var waitIndicationService = _serviceContainer.GetService<IRichWaitIndicationService>();

            await waitIndicationService.BeginAsync(initialMessage);

            try
            {
                using var server = RichDocumentPreviewServer.Create(_serviceContainer);

                await waitIndicationService.UpdateAsync("جاري تحميل نمط الطباعة ...");
                await server.LoadTemplateAsync(templateSource);

                await waitIndicationService.UpdateAsync("جاري جلب البيانات ...");
                await server.FillDataAsync(dataSource);
                await waitIndicationService.EndAsync();

                operation(server);
            }
            catch
            {
                await waitIndicationService.EndAsync();
                throw;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
