using CA.Domain.Entities;
using CA.Domain.Repositories;
using CA.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CA.Persistence.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Non Asynchronous Function
        public IQueryable<TEntity> GetAll(bool isUntrackEntities = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFn = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (isUntrackEntities)
            {
                query = query.AsNoTracking();
            }

            if (includeFn is not null)
            {
                query = includeFn(query);
            }

            return query;
        }

        public TEntity FindById(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> ExecuteSqlRaw(string query)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(query);
        }

        public bool IsDbUpdateConcurrencyException(Exception e)
        {
            return e is DbUpdateConcurrencyException;
        }

        public void SetRowVersion(TEntity entity, byte[] version)
        {
            _dbContext.Entry(entity).OriginalValues[nameof(entity.RowVersion)] = version;
        }

        #endregion

        #region Asynchronous Function

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false)
        {
            IQueryable<TEntity> query = GetAll(isUntrackEntities);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false)
        {
            IQueryable<TEntity> query = GetAll(isUntrackEntities);
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query)
        {
            return await query.ToListAsync();
        }

        #endregion
    }
}
