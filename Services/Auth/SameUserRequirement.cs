using Microsoft.AspNetCore.Authorization;

namespace RecipeHubAPI.Services.Auth
{
    public sealed class SameUserRequirement : IAuthorizationRequirement {}
}
