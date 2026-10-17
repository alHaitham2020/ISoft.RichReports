using DevExpress.XtraPrinting;
using ISoft.RichReportDesigner.Core.Printing;
using ISoft.RichReportDesigner.Core.Templates;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public static class RichReportPrintManagerHelper
    {
        public static async Task ShowPrintPreviewAsync(
            this IRichPrintManager printManager,
            IRichReportTemplate template,
            object dataSource)
        {
            await printManager.ShowPrintPreviewAsync(template, dataSource, template.GetPreviewTitle());
        }

        public static async Task ShowPrintDialogAsync(
            this IRichPrintManager printManager,
            IRichReportTemplate template,
            object dataSource)
        {
            await printManager.ShowPrintDialogAsync(template, dataSource);
        }

        public static async Task PrintAsync(
            this IRichPrintManager printManager,
            IRichReportTemplate template,
            object dataSource)
        {
            await printManager.PrintAsync(template, dataSource);
        }

        public static async Task ExportAsync(
            this IRichPrintManager printManager,
            IRichReportTemplate template,
            object dataSource,
            ExportFormat format,
            string defaultFileName = null)
        {
            await printManager.ExportAsync(template, dataSource, format, defaultFileName);
        }
    }

}
