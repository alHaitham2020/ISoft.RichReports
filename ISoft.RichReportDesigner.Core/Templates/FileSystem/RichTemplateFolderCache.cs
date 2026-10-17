using ISoft.Utils.Caching;

namespace ISoft.RichReportDesigner.Core.Templates.FileSystem
{
    public sealed class RichTemplateFolderCache : ConcurrentCacheDictionary<IRichTemplateFolderKey, IRichTemplateFolder>, IRichTemplateFolderCache
    {
        public RichTemplateFolderCache()
        {
           
        }
    }
}
