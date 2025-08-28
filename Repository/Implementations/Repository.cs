using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RecipeHubAPI.Database;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;
using System.Reflection;

namespace RecipeHubAPI.Repository.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db) 
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        protected async Task<int> CountEntities(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            int total;

            if (filter is not null)
            {
                total = await query.CountAsync(filter);
            }
            else
            {
                total = await query.CountAsync();
            }

            return total;
        }

        protected async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, Dictionary<string, int>? paginationParams = null)
        {
            IQueryable<T> query = dbSet;

            if (filter is not null) query = query.Where(filter);

            if(paginationParams is not null)
            {
                query = query.Skip((int)paginationParams["skip"]).Take((int)paginationParams["rows"]);
            }
            return await query.ToListAsync();
        }

        protected async Task<T?> GetEntity(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        protected async Task<T> CreateEntity(T newEntity)
        {
            dbSet.Add(newEntity);
            await _db.SaveChangesAsync();
            return newEntity;

        }
        protected async Task<List<T>> CreateEntities(List<T> newEntities)
        {
            if (newEntities is null || newEntities.Count == 0) throw new Exception("Entities cannot be null or empty.");
            dbSet.AddRange(newEntities);
            await _db.SaveChangesAsync();
            return newEntities;
        }
        protected async Task<List<K>> CreateForeignEntities<K>(List<K> newEntities) where K : class
        {
            if (newEntities is null || newEntities.Count == 0) throw new Exception("Entities cannot be null or empty.");
            _db.Set<K>().AddRange(newEntities);
            await _db.SaveChangesAsync();
            return newEntities;
        }

        protected async Task DeleteEntities(List<T> entities)
        {
            dbSet.RemoveRange(entities);
            await _db.SaveChangesAsync();
        }
        protected async Task<bool> DeleteEntities(T entity)
        {
            if (entity is not null)
            {
                dbSet.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        protected async Task<T> UpdateEntity<K>(T entity, K entityDTO, bool updateAllFields = false) where K : class
        {
            if (entity is null || entityDTO is null) throw new Exception("Entities cannot be null.");

            if (updateAllFields is true)
            {
                dbSet.Entry(entity).CurrentValues.SetValues(entityDTO);
            }
            else
            {
                Type entityDTOType = typeof(K);
                PropertyInfo[] dtoProperties = entityDTOType.GetProperties();

                // Use EF's SetValues to set all properties
                dbSet.Entry(entity).CurrentValues.SetValues(entityDTO);

                // Then mark only non-null properties as modified
                foreach (PropertyInfo dtoProperty in dtoProperties)
                {
                    object? dtoValue = dtoProperty.GetValue(entityDTO);
                    if (dtoValue is not null)
                    {
                        dbSet.Entry(entity).Property(dtoProperty.Name).IsModified = true;
                    }
                    else
                    {
                        dbSet.Entry(entity).Property(dtoProperty.Name).IsModified = false;
                    }
                }
            }
            await _db.SaveChangesAsync();
            return entity;
        }

        // Execute operations within a database transaction
        public async Task<TResult> ExecuteInTransaction<TResult>(Func<Task<TResult>> operation)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await operation();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // For operations that don't return a value
        public async Task ExecuteInTransaction(Func<Task> operation)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                await operation();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
