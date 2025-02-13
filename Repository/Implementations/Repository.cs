using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RecipeHubAPI.Database;
using System.Linq.Expressions;
using System.Reflection;

namespace RecipeHubAPI.Repository.Implementations
{
    public abstract class Repository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db) 
        {
            _db = db;
            dbSet = _db.Set<T>();
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
            _db.Set<T>().Add(newEntity);
            await _db.SaveChangesAsync();
            return newEntity;

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
        protected async Task<T> UpdateEntity<K>(T entity, K entityDTO, bool updateAllFields = false) where K : class, new()
        {
            if (entity is null || entityDTO is null) throw new Exception("Entities cannot be null.");

            if (updateAllFields is true)
            {
                dbSet.Entry(entity).CurrentValues.SetValues(entityDTO);
            }
            else
            {
                Type entityDTOType = typeof(K);
                Type entityType = typeof(T);

                PropertyInfo[] dtoProperties = entityDTOType.GetProperties();

                foreach (PropertyInfo dtoProperty in dtoProperties)
                {
                    // retrieves the value of the property from the DTO instance.
                    object? dtoValue = dtoProperty.GetValue(entityDTO);

                    if (dtoValue is not null)
                    {
                        PropertyInfo? entityProperty = entityType.GetProperty(dtoProperty.Name);

                        entityProperty?.SetValue(entity, dtoValue);
                    }
                }
            }
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
