using CA.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CA.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> : IConcurrencyHandler<TEntity>
        where TEntity : BaseEntity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        #region Non Asynchronous Function
        IQueryable<TEntity> GetAll(bool isUntrackEntities = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFn = null);

        TEntity FindById(TKey id);

        void Delete(TEntity entity);

        void DeleteRange(IList<TEntity> entities);

        IQueryable<TEntity> ExecuteSqlRaw(string query);

        #endregion

        #region Asynchronous Function
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> FirstOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false);

        Task<TEntity> SingleOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false);

        Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query);
        #endregion
    }
}
