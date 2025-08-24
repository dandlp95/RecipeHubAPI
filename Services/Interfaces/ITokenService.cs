using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Models;
using System.Security.Claims;

namespace RecipeHubAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string GetToken(string username, int userId, bool isAdmin = false);
        Task<ActionResult> TokenValidationResponseAction(Claim userIdClaim, int userId, APIResponse response);
    }
}
