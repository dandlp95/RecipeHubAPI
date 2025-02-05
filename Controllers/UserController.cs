using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.User;
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
    public class UserController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _dbUser;
        private readonly ITokenService _tokenService;
        protected APIResponse _response;
        private readonly IException _exceptionHandler;
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
                IEnumerable<User> users = _dbUser.GetAllUsers();
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
    }
}
