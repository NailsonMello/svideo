using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SVideo.Application.Resources;

namespace SVideo.Test.Settings
{
    public static class ResourceFactory
    {
        public static Resource Create()
        {
            LocalizationOptions localizationOptions = new LocalizationOptions
            {
                ResourcesPath = "Resources"
            };
            IOptions<LocalizationOptions> options = Options.Create(localizationOptions);
            ResourceManagerStringLocalizerFactory factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            return new Resource(factory);
        }
        public static IStringLocalizer<Resource> GetStringLocalizer()
        {
            IOptions<LocalizationOptions> options = Options.Create(new LocalizationOptions());
            ResourceManagerStringLocalizerFactory factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            return new StringLocalizer<Resource>(factory);
        }
    }
}
