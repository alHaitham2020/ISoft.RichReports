using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Provider for a specific image source type
    /// </summary>
    public interface IImageSourceProvider
    {
        /// <summary>
        /// The scheme this provider handles
        /// </summary>
        IImageUriScheme Scheme { get; }

        /// <summary>
        /// Gets a stream for the image
        /// </summary>
        Stream GetStream(string uri);

        /// <summary>
        /// Gets a stream asynchronously
        /// </summary>
        Task<Stream> GetStreamAsync(string uri, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the URI can be handled by this provider
        /// </summary>
        bool CanHandle(string uri);

        /// <summary>
        /// Checks if the image exists
        /// </summary>
        bool Exists(string uri);
    }
}
