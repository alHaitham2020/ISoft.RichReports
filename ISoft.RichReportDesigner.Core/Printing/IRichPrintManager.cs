using DevExpress.XtraPrinting;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public interface IRichPrintManager : IDisposable
    {
        Task PrintAsync(object templateSource, object dataSource);

        Task ShowPrintDialogAsync(object templateSource, object dataSource);

        Task ShowPrintPreviewAsync(object templateSource, object dataSource, string title = null);

        Task ExportAsync(object templateSource, object dataSource, ExportFormat format, string defaultFileName = null);
    }
}
