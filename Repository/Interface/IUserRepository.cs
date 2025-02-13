﻿using RecipeHubAPI.Models;
using RecipeHubAPI.Models.DTO.UserDTOs;

namespace RecipeHubAPI.Repository.Interface
{
    public interface IUserRepository
    {
        Task<UserDTO?> Authenticate(string username, string password);
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task CreateUser(User user);
        Task<User> UpdateUser(UserUpdate userDTO);
        Task DeleteUser(int userId);
    }
}
