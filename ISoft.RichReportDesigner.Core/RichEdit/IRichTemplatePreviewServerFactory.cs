namespace ISoft.RichReportDesigner.Core.RichEdit
{
    /// <summary>
    /// Factory for creating template servers
    /// </summary>
    public interface IRichTemplatePreviewServerFactory
    {
        IRichDocumentPreviewServer Create();
    }
}
