using ISoft.Utils.Resources;
using System;
using System.IO;
using System.Linq;

namespace ISoft.RichReportDesigner.Core.Images
{
    /// <summary>
    /// Provides images from application embedded resources
    /// </summary>
    public class ResourceImageProvider : ImageProviderBase
    {
        public ResourceImageProvider() : base(ImageUriScheme.Resource)
        {
        }

        public override bool Exists(string uri)
        {
            if (!TryParseResourcePath(uri, out var assemblyName, out var resourcePath))
                return false;

            return GetResource(assemblyName, resourcePath).IsResourceExists();
        }

        public override Stream GetStream(string uri)
        {
            if (!TryParseResourcePath(uri, out var assemblyName, out var resourcePath))
                return null;

            try
            {
                return GetResource(assemblyName, resourcePath).GetStream();
            }
            catch (Exception ex) when (ex is IOException or FileNotFoundException or FileLoadException)
            {
                // Log exception
                return null;
            }
        }

        protected virtual IEmbeddedResource GetResource(string assemblyName, string resourcePath)
        {
            if(string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(resourcePath)) return null;

            return EmbeddedResourceLocation.FromLocation(assemblyName, resourcePath, false);
        }

        private bool TryParseResourcePath(string uri, out string assemblyName, out string resourcePath)
        {
            assemblyName = null;
            resourcePath = null;

            if (!ValidateUri(uri, out var fullPath))
                return false;

            var parts = fullPath.Split('/'); // Split into at most 2 parts

            if (parts.Length > 2)
            {
                assemblyName = parts[0];
                resourcePath = string.Join(".", parts.Skip(1)).Replace('/', '.').Replace('\\', '.');
            }
            else
            {
                // Use default assembly, resource path is the full path
                resourcePath = fullPath.Replace('/', '.').Replace('\\', '.');
            }

            return !string.IsNullOrEmpty(resourcePath);
        }
    }
}
