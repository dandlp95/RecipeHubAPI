using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class RecipeController
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        protected APIResponse _response;
        private readonly IExceptionHandler _exceptionHandler;
    }
}
