using System.Linq.Expressions;
using AutoMapper;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository.Implementations
{
    public class RecipeIngredientsRepository : Repository<RecipeIngredient>, IRecipeIngredientsRepository
    {
        private readonly IMapper _mapper;
        public RecipeIngredientsRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }
        public async Task AddRecipeIngredient(RecipeIngredientDTO recipeIngredientDTO)
        {
            RecipeIngredient recipeIngredient = _mapper.Map<RecipeIngredient>(recipeIngredientDTO);
            await CreateEntity(recipeIngredient);
        }
        public async Task AddRecipeIngredients(List<RecipeIngredientDTO> recipeIngredientDTOs)
        {
            List<RecipeIngredient> recipeIngredients = _mapper.Map<List<RecipeIngredient>>(recipeIngredientDTOs);
            await CreateEntities(recipeIngredients);
        }
        public async Task UpdateRecipeIngredient(RecipeIngredientDTO recipeIngredientDTO)
        {
            RecipeIngredient recipeIngredient = _mapper.Map<RecipeIngredient>(recipeIngredientDTO);
            await UpdateEntity(recipeIngredient, recipeIngredientDTO, true);
        }

        public async Task<List<RecipeIngredientDTO>> GetRecipeIngredientsByRecipeId(int recipeId)
        {
            Expression<Func<RecipeIngredient, bool>> filter = e => e.RecipeId == recipeId;
            List<RecipeIngredient> recipeIngredients = await GetAll(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            List<RecipeIngredientDTO> recipeIngredientDTOs = _mapper.Map<List<RecipeIngredientDTO>>(recipeIngredients);
            return recipeIngredientDTOs;
        }

        public async Task DeleteRecipeIngredient(RecipeIngredientDTO recipeIngredientDTO)
        {
            RecipeIngredient recipeIngredient = _mapper.Map<RecipeIngredient>(recipeIngredientDTO);
            await DeleteEntities(recipeIngredient);
        }

        public async Task DeleteIngredientsByRecipeId(int recipeId)
        {
            Expression<Func<RecipeIngredient, bool>> filter = e => e.RecipeId == recipeId;
            List<RecipeIngredient> ingredientsToDelete = await GetAll(filter);
            
            if (ingredientsToDelete.Any())
            {
                await DeleteEntities(ingredientsToDelete);
            }
        }
    }
}
