using AutoMapper;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO;
using RecipeHubAPI.Models.DTO.GroupDTOs;
using RecipeHubAPI.Models.DTO.RecipeDTOs;
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
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
            CreateMap<Group, GroupUpdate>().ReverseMap();
            CreateMap<RecipeDTO, CompleteRecipeDTO>().ReverseMap();
            CreateMap<RecipeIngredient, RecipeIngredientDTO>().ReverseMap();
            CreateMap<Step, StepDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<GroupRecipe, GroupRecipeDTO>().ReverseMap();
        }
    }
}
