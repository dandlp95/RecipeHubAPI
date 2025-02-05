using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.User;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IUserRepository
    {
         UserDTO? Authenticate(string username, string password);
        List<UserDTO> GetUsers();
        User GetUser(int userId);
        Task CreateUser(UserCreateDTO user);
        Task<UserDTO> UpdateUser(UserUpdate userDTO);
        Task DeleteUser(int userId);
    }
}
