using AutoMapper;
using SVideo.Application.AutoMapper.Mappers;

namespace SVideo.Application.AutoMapper
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            ServerMapper.Map(this);
            VideoMapper.Map(this);
        }
    }
}
