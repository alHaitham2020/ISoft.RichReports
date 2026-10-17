using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Printing;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ISoft.RichReportDesigner.Core.Printing
{
    public class RichEditPrinter : XpfRichEditPrinter
    {
        public RichEditPrinter(IRichEditControl control)
            : base(control) 
        {
        
        }

        public void PrintToMyPrinter()
        {
            PrintDialog pDialog = new();

            var queues = new PrintServer().GetPrintQueues([ EnumeratedPrintQueueTypes.Local,EnumeratedPrintQueueTypes.Connections ]);

            System.Collections.IEnumerator localPrinterEnumerator = queues.GetEnumerator();
            PrintQueue printQueue = null;

            if (localPrinterEnumerator.MoveNext())
            {
                printQueue = (PrintQueue)localPrinterEnumerator.Current;
            }

            if (printQueue != null)
            {
                pDialog.PrintQueue = printQueue;
                FixedDocument document = CreateFixedDocument();
                pDialog.PrintDocument(document.DocumentPaginator, string.Empty);
            }
        }
    }
}
