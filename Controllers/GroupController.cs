using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.GroupDTOs;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class GroupController : BaseController
    {
        private readonly IGroupRepository _dbGroup;
        private readonly ITokenService _tokenService;
        private readonly IExceptionHandler _exceptionHandler;

        public GroupController(IMapper mapper, IGroupRepository db, ITokenService tokenService, IExceptionHandler exceptionHandler)
        {
            _dbGroup = db;
            _tokenService = tokenService;
            _exceptionHandler = exceptionHandler;
        }

        [HttpGet("groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetGroups()
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                List<GroupDTO> groups = [];
                groups = await _dbGroup.GetGroups(userId);

                response.Result = groups;
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

        [HttpGet("groups/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetGroup(int groupId)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }
                
                GroupDTO? group = await _dbGroup.GetGroup(groupId, userId);
                response.Result = group;
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

        [HttpPost("groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> CreateGroup([FromBody] GroupUpdate newGroup)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                GroupDTO createdGroup = await _dbGroup.CreateGroup(newGroup, userId);

                response.Result = createdGroup;
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

        [HttpDelete("groups/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> DeleteGroup(int groupId)
        {
            //await _dbGroup.UpdateGroup(new GroupUpdate(), groupId, userId, false);
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                await _dbGroup.DeleteGroup(groupId, userId);

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
        [HttpPut("groups/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> UpdateGroup(int groupId, [FromBody] GroupUpdate groupDto)
        {
            APIResponse response = new();
            try
            {
                var (isValid, userId, errorResponse) = GetUserIdFromClaims();
                if (!isValid)
                {
                    return errorResponse;
                }

                GroupDTO updatedGroup = await _dbGroup.UpdateGroup(groupDto, groupId, userId, false);

                response.Result = updatedGroup;
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

