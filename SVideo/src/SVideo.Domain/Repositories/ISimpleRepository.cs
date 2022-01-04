using SVideo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVideo.Domain.Repositories
{
    public interface ISimpleRepository<T> : IDisposable where T : SimpleClass
    {
        Task<IList<T>> FindAll();
        Task<T> Find(int id);
        Task<bool> InsertAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        Task<bool> DeleteAsync(T obj);
        Task<bool> ExistsById(int id);
    }
}
