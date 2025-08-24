using Microsoft.AspNetCore.Authorization;

namespace RecipeHubAPI.Services.Auth
{
    public class SameUserHandler : AuthorizationHandler<SameUserRequirement>
    {
        private readonly IHttpContextAccessor _http;
        public SameUserHandler(IHttpContextAccessor http) => _http = http;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement)
        {
            // Admin bypass (optional)
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Grab userId from claims
            var claimUserId = context.User.FindFirst("userId")?.Value;
            if (string.IsNullOrWhiteSpace(claimUserId))
                return Task.CompletedTask; // fail (no claim)

            // Grab {userId} from route
            var routeUserIdStr = _http.HttpContext?.Request.RouteValues["userId"]?.ToString();
            if (string.IsNullOrWhiteSpace(routeUserIdStr))
                return Task.CompletedTask; // fail (no route param)

            // Compare as ints; adjust if your IDs are GUID/strings
            if (int.TryParse(claimUserId, out var claimId) &&
                int.TryParse(routeUserIdStr, out var routeId) &&
                claimId == routeId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
