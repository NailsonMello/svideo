using AutoMapper;
using SVideo.API.AutoMapper;
using SVideo.Application.AutoMapper;

namespace SVideo.Test.Settings
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
                mc.AddProfile(new ApplicationAutoMapperProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
