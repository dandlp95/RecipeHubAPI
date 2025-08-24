using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO;

namespace RecipeHubAPI.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO> GetCategoryById(int categoryId, int userId);
        Task<List<CategoryDTO>> GetCategories(int userId);
        Task<List<CategoryDTO>> GetCategoryByRecipeId(int recipeId, int userId);
        Task<CategoryDTO> CreateCategory(CategoryDTO category);
        Task DeleteCategoryById(int categoryId, int userId);
        Task DeleteCategoryByRecipeId(int recipeId, int userId);
        Task<CategoryDTO> UpdateCategory(CategoryDTO category);
    }
}
