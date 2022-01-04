using AutoMapper;
using SVideo.API.AutoMapper.Mappers;

namespace SVideo.API.AutoMapper
{
    /// <summary>
    /// Auto Mapper Profile Class
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Mapper Class Constructor
        /// </summary>
        public AutoMapperProfile()
        {
            ServerMapper.Map(this);
            VideoMapper.Map(this);
        }
    }
}
