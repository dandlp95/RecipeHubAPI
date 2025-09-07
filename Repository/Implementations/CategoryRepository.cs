using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;

namespace RecipeHubAPI.Repository.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await CreateEntity(category);
            return category;
        }

        public async Task CreateCategories(List<Category> categories)
        {
            await CreateEntities(categories);
        }

        public async Task<List<Category>> GetCategories(int userId)
        {
            Expression<Func<Category, bool>> expression = c => c.Recipe.User.UserId == userId;
            List<Category> categories = await GetAll(expression);
            return categories;
        }

        public async Task DeleteCategoryById(int categoryId, int userId)
        {
            Expression<Func<Category, bool>> expression = c => c.CategoryId == categoryId && c.Recipe.User.UserId == userId;
            Category category = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified CategoryId not found.");
            await DeleteEntities(category);
        }

        public async Task DeleteCategoryByRecipeId(int recipeId, int userId)
        {
            Expression<Func<Category, bool>> expression =
                c => c.Recipe.RecipeId == recipeId && c.Recipe.User.UserId == userId;
            List<Category> categories = await GetAll(expression);
            await DeleteEntities(categories);
        }

        public async Task<Category> GetCategoryById(int categoryId, int userId)
        {
            Expression<Func<Category, bool>> expression = c => c.CategoryId == categoryId && c.Recipe.User.UserId == userId;
            Category category = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified CategoryId not found.");
            return category;
        }

        public async Task<List<Category>> GetCategoryByRecipeId(int recipeId, int userId)
        {
            Expression<Func<Category, bool>> expression =
                c => c.Recipe.RecipeId == recipeId && c.Recipe.User.UserId == userId;

            List<Category> categories = await GetAll(expression);
            return categories;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            Expression<Func<Category, bool>> expression = entities => entities.CategoryId == category.CategoryId;
            Category existingCategory = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified CategoryId not found.");
            await UpdateEntity(existingCategory, category);
            return existingCategory;
        }
    }
}
