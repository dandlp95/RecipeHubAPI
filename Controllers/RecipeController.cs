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
        private readonly IRecipeService _recipeService;
        private readonly IMeasurementUnitRepository _measurementUnitRepository;
        public RecipeController(IMapper mapper, IExceptionHandler exceptionHandler, IRecipeRepository dbRecipe, ICategoryRepository categoryRepository, IRecipeService recipeService, IMeasurementUnitRepository measurementUnitRepository)
        {
            _mapper = mapper;
            _exceptionHandler = exceptionHandler;
            _dbRecipe = dbRecipe;
            _categoryRepository = categoryRepository;
            _recipeService = recipeService;
            _measurementUnitRepository = measurementUnitRepository;
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

                CompleteRecipeDTO recipe = await _recipeService.GetRecipeById(recipeId, userId);
                
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
        public async Task<ActionResult<APIResponse>> GetRecipesByGroup([FromQuery] int? groupId = null)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                List<CompleteRecipeDTO> recipes = await _recipeService.GetRecipesByGroupId(userId, groupId);
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
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                recipeCreateDTO.UserId = userId;

                int recipeId = await _recipeService.CreateRecipe(recipeCreateDTO);

                return CreatedAtAction(nameof(GetRecipe), new { userId, recipeId });
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

        [HttpPut("recipes/{recipeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> UpdateRecipe(int recipeId, [FromBody] CompleteRecipeDTO recipeUpdateDTO)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                recipeUpdateDTO.UserId = userId;
                recipeUpdateDTO.RecipeId = recipeId;

                await _recipeService.UpdateRecipe(recipeUpdateDTO, userId);

                response.Result = recipeUpdateDTO;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Errors = null;
                response.IsSuccess = true;
                return Ok(response);

            }catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, response);
            }
        }
        
        [HttpDelete("recipes/{recipeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> DeleteRecipe(int recipeId){
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                await _recipeService.DeleteRecipe(recipeId, userId);

                return NoContent();

            }catch (RecipeHubException ex)
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

        [HttpDelete("recipes/{recipeId}/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> DeleteRecipeCategories(int recipeId)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                await _categoryRepository.DeleteCategoryByRecipeId(recipeId, userId);

                return NoContent();
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


        [HttpGet("measurement-units")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetMeasurementUnits()
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                response.Result = await _measurementUnitRepository.GetMeasurementUnits();
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

    };
}

