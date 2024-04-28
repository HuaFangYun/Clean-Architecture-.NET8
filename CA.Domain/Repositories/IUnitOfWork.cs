namespace CA.Domain.Repositories
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken token = default);
        Task<IDisposable> BeginTransactionAsync(CancellationToken token = default);
        Task CommitTransactionAsync(CancellationToken token = default);
    }
}
