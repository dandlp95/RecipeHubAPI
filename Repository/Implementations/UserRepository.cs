﻿using AutoMapper;
using RecipeHubAPI.Database;
using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.UserDTOs;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Services;
using System.Linq.Expressions;


namespace RecipeHubAPI.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private IMapper _mapper;
        private IPasswordService _passwordService;

        public UserRepository(ApplicationDbContext db, IMapper mapper, IPasswordService passwordHelper) : base(db)
        {
            _mapper = mapper;
            _passwordService = passwordHelper;
        }

        public async Task<UserDTO?> Authenticate(string username, string password)
        {
            User? foundUser = await GetEntity(user => user.UserName == username) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "User not found.");
            bool match = _passwordService.VerifyPassword(password, foundUser.Password, foundUser.PasswordSalt);

            if (match is false)
            {
                throw new RecipeHubException(System.Net.HttpStatusCode.Unauthorized, "Invalid credentials");
            }

            UserDTO responseUser = _mapper.Map<UserDTO>(foundUser);
            return responseUser;
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
        public async Task<User> UpdateUser(UserUpdate userDTO)
        {
            Expression<Func<User, bool>> expression = entity => entity.UserId == userDTO.UserId;
            User user = await GetEntity(expression) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "UserId does not match any entity in database.");

            await UpdateEntity(user, userDTO);
            return user;
        }

        public async Task DeleteUser(int userId)
        {
            User user = await GetEntity(user => user.UserId == userId) ?? throw new RecipeHubException(System.Net.HttpStatusCode.NotFound, "User not found.");
            if (user is not null) await DeleteEntities(user);
        }
    }
}
