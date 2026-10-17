namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    // provide a localized display name for propeyy
    public interface IRichDocumentPropertyDisplayNameProvider
    {
        string GetDisplayName(RichDocumentPropertyId id);
    }
}
