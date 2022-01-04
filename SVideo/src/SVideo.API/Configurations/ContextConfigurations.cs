using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SVideo.Infra.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace SVideo.API.Configurations
{
    /// <summary>
    /// Context Configurations
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ContextConfigurations
    {
        /// <summary>
        /// Logger
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        /// <summary>
        /// Add Custom Data Context
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="config">Configuration</param>
        public static IServiceCollection AddCustomDataContext(this IServiceCollection services, IConfiguration config)
        {
            string sVideoConnection = config["SVideoConnectionString"];

            services.AddScoped((provider) =>
            {
                return new DbContextOptionsBuilder<SVideoContext>()
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(sVideoConnection)
                .Options;
            });


            services.AddDbContext<SVideoContext>();

            return services;
        }
    }
}
