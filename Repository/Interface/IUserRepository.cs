using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.User;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IUserRepository
    {
         UserDTO? Authenticate(string username, string password);
        List<User> GetAllUsers();
        User GetUser(int userId);
        Task CreateUser(User user);
        Task<User> UpdateUser(UserUpdate userDTO);
        Task DeleteUser(int userId);
    }
}
