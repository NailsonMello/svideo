using SVideo.API.Models.Request;
using SVideo.Domain.Dtos;
using AutoM = AutoMapper;

namespace SVideo.API.AutoMapper.Mappers
{
    /// <summary>
    /// Server Mapper
    /// </summary>
    public static class ServerMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<ServerRequest, ServerCreateDto>();
            profile.CreateMap<ServerRequest, ServerUpdateDto>();
        }
    }
}
