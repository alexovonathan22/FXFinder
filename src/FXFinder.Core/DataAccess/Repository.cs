using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        protected WalletDbContext Context;
        private readonly ILogger<Repository<T>> logger;

        #endregion

        public Repository(WalletDbContext context, ILogger<Repository<T>> log)
        {
            logger = log;
            Context = context;
        }

        #region Repository's Public Methods

        public async Task<T> LoadById(int id) => await Context.Set<T>().FindAsync(id);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(Insert)} entity cannot be null");
            try
            {
                await Context.Set<T>().AddAsync(entity);
                await Context.SaveChangesAsync();
                logger.LogInformation($"Successfully Saved {entity}");
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}");
                throw new Exception($"{nameof(entity)} could not be saved: {ex}");
            }
        }
        public async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(Update)} entity cannot be null");
            // In case AsNoTracking is used
            try
            {
                //Context.Attach(entity);
                //Context.Entry(entity).State = EntityState.Modified;
                //Context.Set<T>().Update(entity);

                Context.Update(entity);
                await Context.SaveChangesAsync();

                logger.LogInformation($"Successfully Updated {entity}");
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}");
            }
        }

        public async Task UpdateLists(List<T> entities)
        {
            if (entities == null) throw new ArgumentNullException($"{nameof(Update)} entities cannot be null");
            // In case AsNoTracking is used
            try
            {
                //Context.Attach(entity);
                //Context.Entry(entity).State = EntityState.Modified;
                //Context.Set<T>().Update(entity);

                Context.UpdateRange(entities);
                await Context.SaveChangesAsync();

                logger.LogInformation($"Successfully Updated {entities.Count}");
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}");
            }
        }

        public async Task Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(Remove)} entity cannot be null");
            logger.LogInformation($"Tryiing to  Delete {entity}");
            Context.Set<T>().Remove(entity);
            logger.LogInformation($"Successfully Deleted {entity}");
            await Context.SaveChangesAsync();
        }

        public async Task<List<T>> LoadAll()
        {
            try
            {
                return await Context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}");

                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        // TODO: Implement methods with AsNoTracking for when retreieval of data is the goal and not making changes.
        public async Task<List<T>> LoadWhere(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().AsNoTracking().CountAsync(predicate);

       

        #endregion

    }
}
