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
    public class GroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _dbGroup;
        private readonly ITokenService _tokenService;
        //protected APIResponse _response;
        private readonly IExceptionHandler _exceptionHandler;

        public GroupController(IMapper mapper, IGroupRepository db, ITokenService tokenService, IExceptionHandler exceptionHandler)
        {
            _mapper = mapper;
            _dbGroup = db;
            //_response = new();
            _tokenService = tokenService;
            _exceptionHandler = exceptionHandler;
        }

        [HttpGet("users/{userId}/groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetGroups(int userId)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) return tokenValidationResult;

                List<GroupDTO> groups = [];
                groups = _dbGroup.GetGroups(userId, null);

                //_response.Result = groups;
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

        [HttpGet("users/{userId}/groups/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> GetGroup(int userId, int groupId)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) { return tokenValidationResult; }
                GroupDTO group = _dbGroup.GetGroup(groupId, userId);
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

        [HttpPost("users/{userId}/groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> CreateGroup(int userId, [FromBody] GroupUpdate newGroup)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) { return tokenValidationResult; }
                GroupDTO createdGroup = await _dbGroup.CreateGroup(newGroup);

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

        [HttpDelete("users/{userId}/applications/{applicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "User")]
        public async Task<ActionResult<APIResponse>> DeleteGroup(int userId, int applicationId)
        {
            APIResponse response = new();
            try
            {
                ActionResult tokenValidationResult = _tokenService.TokenValidationResponseAction(User.FindFirst("userId"), userId, response);
                if (tokenValidationResult is not null) { return tokenValidationResult; }
                await _dbGroup.DeleteGroup(userId, applicationId);

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


    }
}
