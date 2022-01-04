using SVideo.API.Models.Request;
using SVideo.Domain.Dtos;
using AutoM = AutoMapper;

namespace SVideo.API.AutoMapper.Mappers
{
    /// <summary>
    /// Video Mapper
    /// </summary>
    public static class VideoMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<VideoRequest, VideoCreateDto>();
        }
    }
}
