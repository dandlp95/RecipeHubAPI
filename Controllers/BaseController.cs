using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Models;

namespace RecipeHubAPI.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Extracts the User ID from the JWT token claims and validates it
        /// </summary>
        /// <returns>Tuple containing (isValid, userId, response). If isValid is false, response contains the error response</returns>
        /// <example>
        /// <code>
        /// var (isValid, userId, errorResponse) = GetUserIdFromClaims();
        /// if (!isValid)
        /// {
        ///     return errorResponse;
        /// }
        /// // Use userId here...
        /// </code>
        /// </example>
        protected (bool isValid, int userId, ActionResult<APIResponse>? response) GetUserIdFromClaims()
        {
            string claimUserId = User.FindFirst("userId")?.Value ?? "0";
            
            if (!int.TryParse(claimUserId, out int userId) || userId == 0)
            {
                var response = new APIResponse
                {
                    Result = null,
                    StatusCode = System.Net.HttpStatusCode.Unauthorized,
                    Errors = new List<string>() { "User not found." },
                    IsSuccess = false
                };
                
                return (false, 0, Unauthorized(response));
            }
            
            return (true, userId, null);
        }
    }
}
