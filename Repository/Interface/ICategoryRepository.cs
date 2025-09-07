using RecipeHubAPI.Models;

namespace RecipeHubAPI.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryById(int categoryId, int userId);
        Task<List<Category>> GetCategories(int userId);
        Task<List<Category>> GetCategoryByRecipeId(int recipeId, int userId);
        Task<Category> CreateCategory(Category category);
        Task CreateCategories(List<Category> categories);
        Task DeleteCategoryById(int categoryId, int userId);
        Task DeleteCategoryByRecipeId(int recipeId, int userId);
        Task<Category> UpdateCategory(Category category);
    }
}
