using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace SVideo.API.AutoMapper
{
    /// <summary>
    /// Auto Mapper Setup Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AutoMapperSetup
    {
        /// <summary>
        /// Add Auto Mapper Setup Method
        /// </summary>
        /// <param name="services">Service Collection</param>
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            MapperConfiguration config = AutoMapperConfig.RegisterMapper();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
