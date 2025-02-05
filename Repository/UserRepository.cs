using AutoMapper;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Implementations;
using RecipeHubAPI.Repository.Interface;

namespace RecipeHubAPI.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private IMapper _mapper;
        

    }
}
