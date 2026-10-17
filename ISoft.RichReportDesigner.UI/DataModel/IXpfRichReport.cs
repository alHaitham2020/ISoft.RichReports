using ISoft.RichReportDesigner.Core.Reports;
using ISoft.RichReportDesigner.Core.Templates;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.UI.DataModel
{
    public interface IXpfRichReport : IRichReport
    {
        IXpfRichReportTemplateItemsSource DefaultTemplatesSource { get; }

        IXpfRichReportTemplateItemsSource StoreTemplatesSource { get; }

        IRichReportTemplate DesignerTemplate { get; }

        Task NewTemplateAsync();

        Task CloneAsync(Guid? templateKey);

        Task EditTemplateAsync(Guid? templateKey);

        void EditTemplate(IRichReportTemplate template);

        void ShowTemplatesEditorDialog(string title = null);
    }
}
