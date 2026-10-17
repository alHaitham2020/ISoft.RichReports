using ISoft.Utils.Caching;

namespace ISoft.RichReportDesigner.Core.Templates.FileSystem
{
    public interface IRichTemplateFolderCache : IConcurrentCacheDictionary<IRichTemplateFolderKey, IRichTemplateFolder>
    {
        
    }
}
