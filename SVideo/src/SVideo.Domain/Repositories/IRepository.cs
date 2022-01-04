using SVideo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVideo.Domain.Repositories
{
    public interface IRepository<T> : IDisposable where T : GenericClass
    {
        Task<IList<T>> FindAll();
        Task<T> Find(Guid id);
        Task<bool> InsertAsync(T obj);
        Task<bool> Update(T obj);
        Task<bool> Delete(T obj);
        Task<bool> ExistsById(Guid id);

    }
}
