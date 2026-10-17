using DevExpress.XtraPrinting;
using ISoft.RichReportDesigner.Core.RichEdit;
using ISoft.RichReportDesigner.Core.RichEdit.Services;
using ISoft.Utils.Helpers;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public class RichPrintManager : IRichPrintManager
    {
        private readonly IRichWaitIndicationService _waitIndicationService;
        private readonly IRichTemplatePreviewServerFactory _serverFactory;

        public RichPrintManager(
            IRichWaitIndicationService waitIndicationService,
            IRichTemplatePreviewServerFactory serverFactory)
        {
            _waitIndicationService = waitIndicationService ?? throw new ArgumentNullException(nameof(waitIndicationService));
            _serverFactory = serverFactory ?? throw new ArgumentNullException(nameof(serverFactory));
        }

        public static IRichPrintManager Create(
            IRichWaitIndicationService waitIndicationService,
            IRichTemplatePreviewServerFactory serverFactory)
        {
            return new RichPrintManager(waitIndicationService, serverFactory);
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

            await _waitIndicationService.BeginAsync(initialMessage);

            try
            {
                using var server = _serverFactory.Create();

                await _waitIndicationService.UpdateAsync("جاري تحميل نمط الطباعة ...");
                await server.LoadTemplateAsync(templateSource);

                await _waitIndicationService.UpdateAsync("جاري جلب البيانات ...");
                await server.FillDataAsync(dataSource);

                await _waitIndicationService.EndAsync();

                operation(server);
            }
            catch
            {
                await _waitIndicationService.EndAsync();
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
