using ISoft.RichReportDesigner.Core.Templates;
using System;

namespace ISoft.RichReportDesigner.Core.Designer
{
    public class RichReportDesignerTemplateEventArgs : EventArgs
    {
        private readonly IRichReportTemplate template;

        public IRichReportTemplate Template => template;

        public RichReportDesignerTemplateEventArgs(IRichReportTemplate template)
        {
            this.template = template;
        }
    }
}
