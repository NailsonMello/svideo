using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SVideo.Application.Exceptions;
using SVideo.Domain.Repositories;
using SVideo.Infra.Contexts;
using System;
using System.Linq;

namespace SVideo.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SVideoContext _sVideoContext;

        private IDbContextTransaction _transactionContext;

        public UnitOfWork(SVideoContext sVideoContext)
        {
            _sVideoContext = sVideoContext;
        }


        public void BeginTransactionSip()
        {
            _transactionContext = _sVideoContext.Database.BeginTransaction();
        }

        public void CommitSip()
        {
            try
            {
                _sVideoContext.SaveChanges();
                _transactionContext.Commit();
            }
            finally
            {
                _transactionContext.Dispose();
            }
        }

        public void RollbackSip()
        {
            if (_transactionContext != null)
            {
                _transactionContext.Rollback();
                _sVideoContext.ChangeTracker.Entries()
                   .Where(e => e.Entity != null && e.State == EntityState.Added).ToList()
                   .ForEach(e => e.State = EntityState.Detached);
                try
                {
                    _transactionContext.Dispose();
                }
                catch (Exception e)
                {
                    throw new PersistenceException(e);
                }
            }
        }
    }
}
