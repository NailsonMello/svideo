using Microsoft.EntityFrameworkCore;
using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using SVideo.Domain.Utils;
using SVideo.Infra.Contexts;
using System.Linq;

namespace SVideo.Infra.Repositories
{
    public class RecyclerRepository : SimpleRepository<Recycler>, IRecyclerRepository
    {
        public RecyclerRepository(SVideoContext context) : base(context) { }

        public Recycler Get() => _DbSet.AsQueryable().FirstOrDefault();

        public string StatusRunning()
        => _DbSet.AsQueryable().FirstOrDefault().IsRunning ? Constants.RUNNING : Constants.NOT_RUNNING;

        public void Update(Recycler recycler)
        {
            _context.Entry(recycler).State = EntityState.Modified;
            int result = _context.SaveChanges();
        }
    }
}
