using ISoft.RichReportDesigner.Core.Templates;

namespace ISoft.RichReportDesigner.Core.Designer
{
    public class RichReportDesignerTemplateSavedEventArgs : RichReportDesignerTemplateEventArgs
    {
        private readonly bool isNew;

        public bool IsNew => isNew;

        public RichReportDesignerTemplateSavedEventArgs(IRichReportTemplate template, bool isNew) : base(template) 
        {
            this.isNew = isNew;
        }
    }
}
