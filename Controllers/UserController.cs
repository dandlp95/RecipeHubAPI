using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Controllers
{
    [ApiController]
    [Route("RecipeHub")]
    public class UserController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        
        
    }
}
