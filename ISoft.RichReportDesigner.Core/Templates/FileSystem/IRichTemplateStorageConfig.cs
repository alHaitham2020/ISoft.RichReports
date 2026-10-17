namespace ISoft.RichReportDesigner.Core.Templates.FileSystem
{
    public interface IRichTemplateStorageConfig
    {
        string BasePath { get;}

        string TemplatesFolder { get;}

        string FileExtension { get; }

        string GetTemplatesPath();
    }
}
