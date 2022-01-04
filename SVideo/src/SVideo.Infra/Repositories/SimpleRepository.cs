using Microsoft.EntityFrameworkCore;
using SVideo.Application.Exceptions;
using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using SVideo.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVideo.Infra.Repositories
{
    public class SimpleRepository<T> : ISimpleRepository<T> where T : SimpleClass
    {
        protected readonly SVideoContext _context;
        protected readonly DbSet<T> _DbSet;

        public SimpleRepository(SVideoContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }

        public async Task<IList<T>> FindAll()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T> Find(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<bool> InsertAsync(T obj)
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

        public async Task<bool> UpdateAsync(T obj)
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

        public async Task<bool> DeleteAsync(T obj)
        {
            _DbSet.Remove(obj);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _DbSet.AnyAsync(e => e.Id == id);
        }

    }
}
