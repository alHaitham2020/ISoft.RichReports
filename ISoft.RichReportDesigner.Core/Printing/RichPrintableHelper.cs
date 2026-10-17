using DevExpress.XtraPrinting;
using ISoft.RichReportDesigner.Core.Templates;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public static class RichPrintableHelper
    {
        public static async Task ShowPrintPreviewAsync(
            this IRichPrintable printable,
            IRichReportTemplate template,
            object dataSource)
        {
            using var printManager = RichPrintManager.Create(printable.WaitIndicationService, printable.TemplateServerFactory);
            await printManager.ShowPrintPreviewAsync(template, dataSource, template.GetPreviewTitle());
        }

        public static async Task ShowPrintDialogAsync(
            this IRichPrintable printable,
            IRichReportTemplate template,
            object dataSource)
        {
            using var printManager = RichPrintManager.Create(printable.WaitIndicationService, printable.TemplateServerFactory);
            await printManager.ShowPrintDialogAsync(template, dataSource);
        }

        public static async Task PrintAsync(
            this IRichPrintable printable,
            IRichReportTemplate template,
            object dataSource)
        {
            using var printManager = RichPrintManager.Create(printable.WaitIndicationService, printable.TemplateServerFactory);
            await printManager.PrintAsync(template, dataSource);
        }

        public static async Task ExportAsync(
            this IRichPrintable printable,
            IRichReportTemplate template,
            object dataSource,
            ExportFormat format,
            string defaultFileName = null)
        {
            using var printManager = RichPrintManager.Create(printable.WaitIndicationService, printable.TemplateServerFactory);
            await printManager.ExportAsync(template, dataSource, format, defaultFileName);
        }
    }
}
