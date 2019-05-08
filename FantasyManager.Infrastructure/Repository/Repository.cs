using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FantasyManager.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        #region Local Members

        private readonly DbContext _dbContext = null;

        #endregion

        #region Constructors

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public async Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            return await Get(predicate).SingleOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            return await Get(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            return await Get(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(long id) where TEntity : class
        {
            return await FindAsync<TEntity>(id);
        }

        public async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            return await _dbContext.FindAsync<TEntity>(keyValues);
        }

        public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            return predicate != null ? Set<TEntity>().Where(predicate) : Set<TEntity>();
        }

        public async Task<int> CreateAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            // add all 
            _dbContext.AddRange(entities);

            // persist 
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            // update all 
            _dbContext.UpdateRange(entities);

            // persist 
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            // remove all  
            _dbContext.RemoveRange(entities);

            // persist 
            return await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        #endregion
    }
}
