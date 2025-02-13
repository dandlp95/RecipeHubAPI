using AutoMapper;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.UserDTOs;

namespace RecipeHubAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserUpdate>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
        }
    }
}
