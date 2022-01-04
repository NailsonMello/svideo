using Microsoft.EntityFrameworkCore;
using SVideo.Application.Exceptions;
using SVideo.Domain.Entities;
using SVideo.Domain.Filters;
using SVideo.Domain.Repositories;
using SVideo.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVideo.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : GenericClass
    {
        protected readonly SVideoContext _context;
        protected readonly DbSet<T> _DbSet;

        public Repository(SVideoContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }

        public virtual async Task<IList<T>> FindAll()
        {
            return await _DbSet.ToListAsync();
        }

        public virtual async Task<T> Find(Guid id)
        {
            return await _DbSet.FindAsync(id);
        }

        public virtual async Task<bool> InsertAsync(T obj)
        {
            try
            {
                _DbSet.Add(obj);
                int result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                throw PersistenceException.CheckPersistenceException(ex);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public async Task<bool> Update(T obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                throw PersistenceException.CheckPersistenceException(ex);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public async Task<bool> Delete(T obj)
        {
            _DbSet.Remove(obj);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ExistsById(Guid id)
        {
            return await _DbSet.AnyAsync(e => e.Id == id);
        }

        protected virtual async Task<PagedData<T>> Paginate(IQueryable<T> dbSet, PageFilter filter)
        {
            (int take, int skip) = CalcPageOffset(filter);
            int dataCount = await dbSet.CountAsync();

            List<T> data = await dbSet.Take(take).Skip(skip).ToListAsync();

            int totalPages = 0;
            if (dataCount < filter.PageSize)
            {
                totalPages = 1;
            }
            else
            {
                totalPages = dataCount / filter.PageSize;
                totalPages = dataCount % filter.PageSize > 0 ? totalPages + 1 : totalPages;
            }

            return new PagedData<T>() { PageNumber = filter.PageNumber, PageSize = filter.PageSize, TotalPages = totalPages, TotalRecords = dataCount, Data = data };
        }

        protected (int, int) CalcPageOffset(PageFilter filter)
        {
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            int take = skip + filter.PageSize;
            return (take, skip);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
