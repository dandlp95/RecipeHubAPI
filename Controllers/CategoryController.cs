using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;
using System.Text.RegularExpressions;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IExceptionHandler _exceptionHandler;

        public CategoryController(ICategoryRepository categoryRepository, IExceptionHandler exceptionHandler)
        {
            _categoryRepository = categoryRepository;
            _exceptionHandler = exceptionHandler;
        }

        [HttpGet("categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetCategory(int categoryId)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                
                CategoryDTO categories = await _categoryRepository.GetCategoryById(categoryId, userId);
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

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetCategories()
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                
                List<CategoryDTO> categories = await _categoryRepository.GetCategories(userId);
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

        [HttpDelete("categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> DeleteCategory(int categoryId)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                await _categoryRepository.DeleteCategoryById(categoryId, userId);
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

        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            APIResponse response = new();
            try{
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                categoryDTO.UserId = userId;
                categoryDTO = await _categoryRepository.CreateCategory(categoryDTO);
                int? categoryId = categoryDTO.CategoryId;

                return CreatedAtAction(nameof(GetCategory), new { categoryId });
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

        [HttpPut("categories/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> UpdateCategory(int categoryId, [FromBody] CategoryDTO categoryDTO)
        {
            APIResponse response = new();
            try{
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                categoryDTO.UserId = userId;
                categoryDTO.CategoryId = categoryId;
                categoryDTO = await _categoryRepository.UpdateCategory(categoryDTO);
                response.Result = categoryDTO;
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


    }
}
