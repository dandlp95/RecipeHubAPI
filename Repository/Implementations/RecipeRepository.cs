using AutoMapper;
using RecipeHubAPI.CustomTypes;
using RecipeHubAPI.Database;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.Recipe;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;
using System.Linq.Expressions;

namespace RecipeHubAPI.Repository.Implementations
{
    public class RecipeRepository:Repository<Recipe>, IRecipeRepository
    {
        private IMapper _mapper;
        public RecipeRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<RecipeDTO> CreateRecipe(RecipeCreateDTO recipeCreateDTO)
        {
            Recipe recipe = _mapper.Map<Recipe>(recipeCreateDTO);
            await CreateEntity(recipe);
            RecipeDTO newRecipeDTO = _mapper.Map<RecipeDTO>(recipe);

            return newRecipeDTO;
        }

        public async Task<List<RecipeDTO>> GetRecipes(int userId, int? groupdId = null, PaginationParams? paginationParams = null)
        {
            // Not sure if I will implement pagination for now...
            List<Recipe> results = [];
            Expression<Func<Recipe, bool>> filter;
            if (groupdId == null)
            {
                filter = entity => entity.UserId == userId;
            }
            else
            {
                filter = entity => entity.GroupRecipes.Any(rg => rg.GroupId == groupdId);
            }
            results = await GetAll(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            List<RecipeDTO> resultsDTO = _mapper.Map<List<RecipeDTO>>(results);
            return resultsDTO;
        }
        public async Task<RecipeDTO> GetRecipe(int id, int userId)
        {
            Expression<Func<Recipe, bool>> filter = e => e.UserId == userId && e.RecipeId == id;
            Recipe recipe = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");
            RecipeDTO recipeDTO = _mapper.Map<RecipeDTO>(recipe);
            return recipeDTO;
        }

        public async Task<RecipeDTO> UpdateRecipe(RecipeUpdate recipeDTO, int userId, int recipeId, bool updateAllFields = false) 
        {
            Expression<Func<Recipe, bool>> filter = e => e.RecipeId == recipeId && e.UserId == userId;
            Recipe recipe = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database.");

            await UpdateEntity(recipe, recipeDTO, updateAllFields);
            RecipeDTO recipeUpdate = _mapper.Map<RecipeDTO>(recipe);
            return recipeUpdate;
        } 

        public async Task DeleteRecipe(int id, int userId)
        {
            Expression<Func<Recipe, bool>> filter = e => e.RecipeId == id && e.UserId == userId;
            Recipe recipe = await GetEntity(filter) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "RecipeId doesn't match any entity in the database");
            await DeleteEntities(recipe);
        }

    }
}
