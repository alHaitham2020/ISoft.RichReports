using DevExpress.Office.Services;
using ISoft.RichReportDesigner.Core.Images;
using System;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation
{
    public class RichUriStreamProviderManagerService : IRichUriStreamProviderManagerService
    {
        public RichUriStreamProviderManagerService()
        {

        }

        public virtual void RegisterUriStreamProviders(IUriStreamService uriStreamService)
        {
            //uriStreamService.RegisterProvider(new Base64ImageProvider());
            //uriStreamService.RegisterProvider(new FileSystemImageProvider());
            //uriStreamService.RegisterProvider(new ResourceImageProvider());

            var providersManager = new RichImageUriStreamProvidersManager(new RichImageUriStreamProvidersOptions
            {
                EnableCaching = true,
                MaxCacheItems = 100,
                MaxCacheSizeBytes = 100 * 1024 * 1024, // 100 MB
                CacheExpirationTime = TimeSpan.FromMinutes(30),
                OnCacheCleared = () => Console.WriteLine("Cache cleared")
            });

            // Register providers
            providersManager.RegisterProvider(new FileSystemImageProvider())
                            .RegisterProvider(new Base64ImageProvider())
                            .RegisterProvider(new ResourceImageProvider())
                            .RegisterProvider(new BarcodeImageProvider())
                            .SetDefaultProvider(new FileSystemImageProvider());

            uriStreamService.RegisterProvider(providersManager);

        }

        public virtual void UnRegisterUriStreamProviders(IUriStreamService uriStreamService)
        {

        }
    }
}
