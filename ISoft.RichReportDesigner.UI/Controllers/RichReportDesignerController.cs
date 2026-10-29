using ISoft.RichReportDesigner.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.UI.Controllers
{
    internal class RichReportDesignerController
    {
        private readonly RichReportDesignerControl _control;
        private XpfRichEditorControl _editor;

        public RichReportDesignerController(RichReportDesignerControl control)
        {
            _control = control;
        }

        public void Install(XpfRichEditorControl editor)
        {

        }

        public void UnInstall()
        {

        }
    }
}
