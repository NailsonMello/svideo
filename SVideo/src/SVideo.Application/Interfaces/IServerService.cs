using SVideo.Application.Models;
using SVideo.Application.Models.ViewModels;
using SVideo.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace SVideo.Application.Interfaces
{
    public interface IServerService
    {
        Task<ApplicationResponseStruct<Guid>> CreateAsync(ServerCreateDto dto);
        Task<ApplicationResponseItem<ServerViewModel>> GetAsyncById(Guid id);
        Task<ApplicationResponseList<ServerViewModel>> GetAsyncAll();
        Task<ApplicationResponseStruct<Guid>> UpdateAsync(Guid serverId, ServerUpdateDto dto);
        Task<ApplicationResponseStruct<bool>> IsAvailable(Guid id);
        Task<ApplicationResponseStruct<bool>> DeleteAsync(Guid id);
    }
}
