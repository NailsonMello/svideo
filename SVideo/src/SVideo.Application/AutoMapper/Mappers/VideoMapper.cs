using SVideo.Application.Models.ViewModels;
using SVideo.Domain.Dtos;
using SVideo.Domain.Entities;
using SVideo.Domain.Utils;
using AutoM = AutoMapper;

namespace SVideo.Application.AutoMapper.Mappers
{
    public static class VideoMapper
    {
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<VideoCreateDto, Video>()
                .ForMember(dest => dest.SizeInBytes, opt => opt.MapFrom(src => src.SizeInBytes.FileToByte()))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.SizeInBytes.ContentType));

            profile.CreateMap<Video, DownloadVideoViewModel>();
            profile.CreateMap<Video, VideoViewModel>();
        }
    }
}
