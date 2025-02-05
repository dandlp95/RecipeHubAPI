using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;

namespace RecipeHubAPI.Services.Interfaces
{
    public interface IExceptionHandler
    {
        ActionResult returnExceptionResponse(RecipeHubException ex, APIResponse response);
        ActionResult returnExceptionResponse(Exception ex, APIResponse response);
    }
}
