using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using SVideo.Infra.Contexts;

namespace SVideo.Infra.Repositories
{
    public class ServerRepository : Repository<Server>, IServerRepository
    {
        public ServerRepository(SVideoContext context) : base(context) { }
    }
}
