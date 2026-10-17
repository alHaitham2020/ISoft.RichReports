//using ISoft.RichReportDesigner.Core.Reports;
//using ISoft.RichReportDesigner.Core.RichEdit;
//using ISoft.RichReportDesigner.Core.Templates;
//using System;
//using System.Threading.Tasks;

//namespace ISoft.RichReportDesigner.Core.Printing
//{
//    public class RichReportPrintManager : IRichPrintManager
//    {
//        private readonly IRichTemplateServerFactory _serverFactory;

//        public RichReportPrintManager() : this(RichReportTemplateServerFactory.Create())
//        {

//        }

//        public RichReportPrintManager(IRichTemplateServerFactory serverFactory)
//        {
//            _serverFactory = serverFactory ?? throw new ArgumentNullException(nameof(serverFactory));
//        }

//        public async Task ShowPrintPreviewAsync(IRichReportTemplate template, object dataSource)
//        {
//            using var server = _serverFactory.Create(dataSource);
//            await ExecutePrintOperationAsync(server, template, dataSource);
//            server.ShowPrintPreview(template.GetPreviewTitle());
//        }

//        public async Task ShowPrintDialogAsync(IRichReportTemplate template, object dataSource)
//        {
//            using var server = _serverFactory.Create(dataSource);
//            await ExecutePrintOperationAsync(server, template, dataSource);
//            server.ShowPrintDialog();
//        }

//        public async Task PrintAsync(IRichReportTemplate template, object dataSource)
//        {
//            using var server = _serverFactory.Create(dataSource);
//            await ExecutePrintOperationAsync(server, template, dataSource);
//            server.Print();
//        }

//        private static async Task ExecutePrintOperationAsync(
//            IRichDocumentTemplateServer server,
//            IRichReportTemplate template,
//            object dataSource)
//        {
//            await server.LoadTemplateAsync(template.Content);
//            await server.FillDataAsync(dataSource);
//        }

//        public void Dispose()
//        {
//            // Clean up if needed
//            GC.SuppressFinalize(this);
//        }
//    }
//}
