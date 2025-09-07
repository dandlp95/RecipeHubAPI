using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.UserDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User?> Authenticate(UserLogin credentials);
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
