using SVideo.Domain.Entities;

namespace SVideo.Domain.Repositories
{
    public interface IRecyclerRepository : ISimpleRepository<Recycler>
    {
        string StatusRunning();
        Recycler Get();
        void Update(Recycler recycler);
    }
}
