using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace SVideo.API.Configurations
{
    /// <summary>
    /// Context Configurations
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class StaticFilesConfigurations
    {
        /// <summary>
        /// Add Custom Data Context
        /// </summary>
        /// <param name="app">Appplication Builder</param>
        /// <param name="config">Configuration</param>

        /// <returns>Service Collection</returns>
        public static IApplicationBuilder AddStaticFiles(this IApplicationBuilder app, IConfiguration config)
        {

            string path = config["FileUploadPath"];

            Directory.CreateDirectory(path);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/files"
            });

            return app;
        }
    }
}
