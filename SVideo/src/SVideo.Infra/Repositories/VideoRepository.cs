using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using SVideo.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SVideo.Infra.Repositories
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(SVideoContext context) : base(context) { }

        public List<Video> GetAllByServerId(Guid serverId)
        => _DbSet.AsQueryable().Where(x => x.IdServer == serverId).ToList();

        public Video GetVideoByIdAndServerId(Guid serverId, Guid videoId)
        => _DbSet.AsQueryable().Where(x => x.Id == videoId && x.IdServer == serverId).FirstOrDefault();

        public IList<Video> ListVideoForDays(int days)
        => _DbSet.AsQueryable().Where(x => x.CreatedAt.AddDays(days).Date <= DateTime.UtcNow.Date).ToList();

        public void RemoveAllList(IList<Video> videos)
        {
            _DbSet.RemoveRange(videos);
            _context.SaveChanges();
        }
    }
}
