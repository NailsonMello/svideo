using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SVideo.Application.Interfaces;
using SVideo.Application.Services;
using SVideo.Domain.Repositories;
using SVideo.Infra.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace SVideo.API.Configurations
{
    /// <summary>
    /// Dependency Injection Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Dependency injection Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigServices();
            services.ConfigRepositories();
            return services;
        }

        private static void ConfigServices(this IServiceCollection services)
        {
            services.AddScoped<IAbstractValidatorService, AbstractValidatorService>();
            services.AddScoped<IServerService, ServerService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IRecyclerService, RecyclerService>();
        }

        private static void ConfigRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServerRepository, ServerRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IRecyclerRepository, RecyclerRepository>();
        }
    }
}
