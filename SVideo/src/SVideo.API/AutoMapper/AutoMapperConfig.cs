using AutoMapper;
using SVideo.Application.AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace SVideo.API.AutoMapper
{
    /// <summary>
    /// Auto Mapper Config Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfig
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        protected AutoMapperConfig() { }

        /// <summary>
        /// Register Mapper Method
        /// </summary>
        /// <returns>Mapper Configuration</returns>
        public static MapperConfiguration RegisterMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
                cfg.AddProfile(new ApplicationAutoMapperProfile());
            });
        }
    }
}
