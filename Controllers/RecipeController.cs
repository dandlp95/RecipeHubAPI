using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.Recipe;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class RecipeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IRecipeRepository _dbRecipe;

        public RecipeController(IMapper mapper, ITokenService tokenService, IExceptionHandler exceptionHandler, IRecipeRepository dbRecipe)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _exceptionHandler = exceptionHandler;
            _dbRecipe = dbRecipe;
        }
        [HttpGet("users/{userId}/recipes/{recipeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetRecipe(int userId, int recipeId)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = await _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) return tokenValidationResult;
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

        [HttpGet("users/{userId}/recipes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetRecipes(int userId, [FromQuery] int? groupId = null)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = await _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) return tokenValidationResult;

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

        [HttpPost("users/{userId}/recipes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRecipe(int userId, [FromBody] Models.DTO.Recipe.RecipeStepsDTO recipeCreateDTO)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = await _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) return tokenValidationResult;
                RecipeDTO newRecipe = await _dbRecipe.CreateRecipe(recipeCreateDTO);
                response.Result = newRecipe;
                response.StatusCode = System.Net.HttpStatusCode.Created;
                response.Errors = null;
                response.IsSuccess = true;
                return CreatedAtAction(nameof(GetRecipe), new { userId, recipeId = newRecipe.RecipeId }, response);
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

