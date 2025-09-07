using RecipeHubAPI.Database;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Services;
using System.Linq.Expressions;
using RecipeHubAPI.Models.DTO.UserDTOs;

namespace RecipeHubAPI.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private IPasswordService _passwordService;

        public UserRepository(ApplicationDbContext db, IPasswordService passwordHelper) : base(db)
        {
            _passwordService = passwordHelper;
        }

        public async Task<User?> Authenticate(UserLogin credentials)
        {
            string? username = credentials.UserName;
            string? email = credentials.Email;
            string password = credentials.Password;

            if (username is null && email is null)
            {
                throw new RecipeHubException(System.Net.HttpStatusCode.BadRequest, "Username or email must be provided.");
            }

            User? foundUser = await GetEntity(user => user.UserName == username || user.EmailAddress == email) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "User not found.");
            bool match = _passwordService.VerifyPassword(password, foundUser.Password, foundUser.PasswordSalt);

            if (match is false)
            {
                throw new RecipeHubException(System.Net.HttpStatusCode.Unauthorized, "Invalid credentials");
            }

            return foundUser;
        }
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await GetAll();
            return users;
        }
        public async Task<User> GetUser(int userId)
        {
            User user = await GetEntity(user => user.UserId == userId) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "User not found.");
            return user;
        }
        public async Task CreateUser(User user)
        {
            user.Password = _passwordService.HashPassword(user.Password, out byte[] salt);
            user.PasswordSalt = salt;
            await CreateEntity(user);
        }
        public async Task<User> UpdateUser(User user)
        {
            Expression<Func<User, bool>> expression = entity => entity.UserId == user.UserId;
            User existingUser = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "UserId does not match any entity in database.");

            await UpdateEntity(existingUser, user);
            return existingUser;
        }

        public async Task DeleteUser(int userId)
        {
            User user = await GetEntity(user => user.UserId == userId) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "User not found.");
            if (user is not null) await DeleteEntities(user);
        }
    }
}
