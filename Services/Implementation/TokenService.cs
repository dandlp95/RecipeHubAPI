﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecipeHubAPI.Models;
using RecipeHubAPI.Repository.Interface;
using RecipeHubAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeHubAPI.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public TokenService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public string GetToken(string username, int userId, bool isAdmin = false)
        {
            var secretKey = _configuration["AppSettings:SecretKey"];
            var apiURL = _configuration["AppSettings:ApiUrl"];
            var Claims = new List<Claim>
            {
                new Claim("username", username),
                new Claim("userId", userId.ToString())
            };

            // Adds the correct authorization type based on user role.
            if (isAdmin) Claims.Add(new Claim("type", "admin"));
            else Claims.Add(new Claim("type", "User"));

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var Token = new JwtSecurityToken(
                apiURL,
                apiURL,
                Claims,
                expires: DateTime.Now.AddDays(15.0),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public int ValidateUserIdToken(Claim userIdClaim, int userId)
        {
            if (Int32.TryParse(userIdClaim?.Value, out int userIdClaimValue))
            {
                // Add logic later to verify their user role.
                if (userIdClaimValue != userId) return -1;
                User user = _userRepository.GetUser(userId);
                return user is not null ? 1 : 0;
            }
            return 0;
        }
        public ActionResult? HandleValidateUserIdToken(int validateTokenResponse, APIResponse response)
        {
            if (validateTokenResponse == 0)
            {
                response.Result = null;
                response.Errors = new List<string>() { "User not found." };
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.IsSuccess = false;
                return new NotFoundObjectResult(response);
            }
            else if (validateTokenResponse == -1)
            {
                return new ForbidResult("Bearer");
            }
            return null;
        }
        public ActionResult TokenValidationResponseAction(Claim userIdClaim, int userId, APIResponse response)
        {
            int validateTokenResponse = ValidateUserIdToken(userIdClaim, userId);
            return HandleValidateUserIdToken(validateTokenResponse, response);
        }
    }
}
