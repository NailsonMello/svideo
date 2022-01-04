using SVideo.Application.Models;
using SVideo.Application.Models.ViewModels;
using SVideo.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace SVideo.Application.Interfaces
{
    public interface IVideoService
    {
        Task<ApplicationResponseStruct<Guid>> CreateVideoAsync(Guid id, VideoCreateDto dto);
        DownloadVideoViewModel Download(Guid serverId, Guid videoId);
        Task<ApplicationResponseStruct<bool>> DeleteAsync(Guid serverId, Guid videoId);
        ApplicationResponseList<VideoViewModel> GetAllByServerId(Guid serverId);
        ApplicationResponseItem<VideoViewModel> GetByIdAndServerId(Guid serverId, Guid videoId);
    }
}
