using DevExpress.Office.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Base class for image providers
    /// </summary>
    public abstract class ImageProviderBase : IImageSourceProvider, IUriStreamProvider
    {
        private readonly IImageUriScheme _scheme;

        protected ImageProviderBase(IImageUriScheme scheme)
        {
            _scheme = scheme;
        }

        public IImageUriScheme Scheme => _scheme;

        public virtual bool CanHandle(string uri)
        {
            return !string.IsNullOrWhiteSpace(uri) && _scheme.CanHandle(uri);
        }

        public abstract bool Exists(string uri);

        public abstract Stream GetStream(string uri);

        public virtual async Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken = default)
        {
            // Default implementation runs synchronously - override for true async
            return await Task.FromResult(GetStream(uri)).ConfigureAwait(false);
        }

        protected bool ValidateUri(string uri, out string path)
        {
            return _scheme.TryParsePath(uri, out path);
        }
    }
}
