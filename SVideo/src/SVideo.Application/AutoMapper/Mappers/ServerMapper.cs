using SVideo.Application.Models.ViewModels;
using SVideo.Domain.Dtos;
using SVideo.Domain.Entities;
using AutoM = AutoMapper;

namespace SVideo.Application.AutoMapper.Mappers
{
    public static class ServerMapper
    {
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<Server, ServerViewModel>();
            profile.CreateMap<ServerCreateDto, Server>();
            profile.CreateMap<ServerUpdateDto, Server>();
        }
    }
}
