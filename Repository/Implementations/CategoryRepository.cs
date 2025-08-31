using AutoMapper;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;

namespace RecipeHubAPI.Repository.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public IMapper _mapper;
        public ApplicationDbContext _db;
        public CategoryRepository(IMapper mapper, ApplicationDbContext db) : base(db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            Category newCategory = _mapper.Map<Category>(category);
            await CreateEntity(newCategory);
            category.CategoryId = newCategory.CategoryId;
            
            return category;
        }

        public async Task<List<CategoryDTO>> GetCategories(int userId)
        {
            Expression<Func<Category, bool>> expression = c => c.Recipe.User.UserId == userId;
            List<Category> categories = await GetAll(expression);
            List<CategoryDTO> categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDTO;
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

        public async Task<CategoryDTO> GetCategoryById(int categoryId, int userId)
        {
            Expression<Func<Category, bool>> expression = c => c.CategoryId == categoryId && c.Recipe.User.UserId == userId;
            Category category = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified CategoryId not found.");
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public async Task<List<CategoryDTO>> GetCategoryByRecipeId(int recipeId, int userId)
        {

            List<Category> categories = new();

            Expression<Func<Category, bool>> expression =
                c => c.Recipe.RecipeId == recipeId && c.Recipe.User.UserId == userId;

            categories = await GetAll(expression);
            List<CategoryDTO> categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDTO;
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO)
        {
            Expression<Func<Category, bool>> expression = entities => entities.CategoryId == categoryDTO.CategoryId;
            Category category = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "Entity with specified CategoryId not found.");
            await UpdateEntity(category, categoryDTO);
            CategoryDTO groupDTO = _mapper.Map<CategoryDTO>(category);
            return groupDTO;
        }
    }
}
