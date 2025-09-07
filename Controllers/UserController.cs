using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.UserDTOs;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;
using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecipeHubAPI.Exceptions;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _dbUser;
        private readonly ITokenService _tokenService;
        protected APIResponse _response;
        private readonly IExceptionHandler _exceptionHandler;
        public UserController(IMapper mapper, IUserRepository dbUser, ITokenService tokenService, IExceptionHandler exceptionHandler)
        {
            _mapper = mapper;
            _dbUser = dbUser;
            _response = new();
            _tokenService = tokenService;
            _exceptionHandler = exceptionHandler;
        }

        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetUsers()
        {
            try
            {
                IEnumerable<User> users = await _dbUser.GetAllUsers();
                _response.Result = _mapper.Map<List<UserDTO>>(users);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
        }

        [HttpGet("users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetUserById(int id)
        {
            try
            {
                if (id < 1)
                {
                    throw new RecipeHubException(HttpStatusCode.BadRequest, "Invalid Id.");
                }
                User fetchedUser = await _dbUser.GetUser(id);
                if (fetchedUser == null)
                {
                    return NotFound(_response);
                }
                UserDTO userDTO = _mapper.Map<UserDTO>(fetchedUser);
                return Ok(userDTO);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
        }

        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddUser([FromBody] UserCreateDTO userCreateDTO)
        {
            try
            {
                User? user = _mapper.Map<User>(userCreateDTO);
                await _dbUser.CreateUser(user);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                UserDTO userInfoResponse = _mapper.Map<UserDTO>(user);
                _response.Result = userInfoResponse;
                _response.Token = _tokenService.GetToken(user.UserName, user.UserId);

                return Ok(_response);
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
        }

        [HttpPost("users/auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] UserLogin userInfo)
        {
            try
            {
                if (userInfo.Email is null && userInfo.UserName is null)
                {
                    throw new RecipeHubException(HttpStatusCode.BadRequest, "Invalid credentials.");
                }

                User? authenticatedUser = await _dbUser.Authenticate(userInfo);
                if (authenticatedUser is null)
                {
                    return Unauthorized();
                }
                UserDTO authenticatedUserDTO = _mapper.Map<UserDTO>(authenticatedUser);
                string userToken = _tokenService.GetToken(authenticatedUserDTO.UserName, authenticatedUserDTO.UserId);
                _response.IsSuccess = true;
                _response.Token = userToken;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Errors = null;
                _response.Result = authenticatedUserDTO;

                return Ok(_response);

            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
        }

        [HttpDelete("users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteUser(int userId)
        {
            try
            {
                await _dbUser.DeleteUser(userId);
                return NoContent();
            }
            catch (RecipeHubException ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
            catch (Exception ex)
            {
                return _exceptionHandler.returnExceptionResponse(ex, _response);
            }
        }
    }
}
