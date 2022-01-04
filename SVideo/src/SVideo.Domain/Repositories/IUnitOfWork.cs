namespace SVideo.Domain.Repositories
{
    public interface IUnitOfWork
    {
        void BeginTransactionSip();
        void CommitSip();
        void RollbackSip();
    }
}
