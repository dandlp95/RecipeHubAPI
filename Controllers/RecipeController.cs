using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class RecipeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IRecipeRepository _dbRecipe;
        private readonly ICategoryRepository _categoryRepository;

        public RecipeController(IMapper mapper, IExceptionHandler exceptionHandler, IRecipeRepository dbRecipe, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
            _dbRecipe = dbRecipe;
            _categoryRepository = categoryRepository;
        }
        [HttpGet("recipes/{recipeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetRecipe(int recipeId)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                RecipeDTO recipe = await _dbRecipe.GetRecipe(recipeId, userId);

                response.Result = recipe;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Errors = null;
                response.IsSuccess = true;

                return Ok(response);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
        }

        [HttpGet("recipes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetRecipes([FromQuery] int? groupId = null)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                List<RecipeDTO> recipes = await _dbRecipe.GetRecipes(userId, groupId);
                response.Result = recipes;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Errors = null;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
        }

        [HttpPost("recipes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> CreateRecipe([FromBody] CompleteRecipeDTO recipeCreateDTO)
        {
            APIResponse response = new();
            throw new NotImplementedException();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                // RecipeStepsDTO newRecipe = await _dbRecipe.CreateRecipe(recipeCreateDTO);
                // response.Result = newRecipe;
                // response.StatusCode = System.Net.HttpStatusCode.Created;
                // response.Errors = null;
                // response.IsSuccess = true;
                // return CreatedAtAction(nameof(GetRecipe), new { userId, recipeId = newRecipe.RecipeId }, response);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
        }

        [HttpGet("recipes/{recipeId}/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetRecipeCategories(int recipeId)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                List<CategoryDTO> categories = [];
                categories = await _categoryRepository.GetCategoryByRecipeId(recipeId, userId);
                response.Result = categories;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Errors = null;
                response.IsSuccess = true;

                return Ok(response);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
        }

        //[HttpPost("users/{userId}/recipes/{recipeId}/step")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> CreateStep

    };
}

