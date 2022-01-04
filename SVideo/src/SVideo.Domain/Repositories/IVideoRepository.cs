using SVideo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SVideo.Domain.Repositories
{
    public interface IVideoRepository : IRepository<Video>
    {
        Video GetVideoByIdAndServerId(Guid serverId, Guid videoId);
        List<Video> GetAllByServerId(Guid serverId);
        IList<Video> ListVideoForDays(int days);
        void RemoveAllList(IList<Video> videos);
    }
}
