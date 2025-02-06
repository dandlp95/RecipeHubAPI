using Microsoft.AspNetCore.Mvc;
using RecipeHubAPI.Exceptions;
using RecipeHubAPI.Models;
using RecipeHubAPI.Services.Interfaces;

namespace RecipeHubAPI.Services.Implementation
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ExceptionHandler() { }

        public ActionResult returnExceptionResponse(RecipeHubException ex, APIResponse response)
        {
            Exception? innerException = ex.InnerException;

            response.Result = null;
            response.Errors = new List<string>() { ex.Message };
            if (innerException != null)
            {
                response.Errors.Add(innerException.Message);
            }
            response.StatusCode = ex.StatusCode;
            response.Result = null;
            response.Token = null;

            return new ObjectResult(response)
            {
                StatusCode = (int)ex.StatusCode
            };
        }

        public ActionResult returnExceptionResponse(Exception ex, APIResponse response)
        {
            Exception? innerException = ex.InnerException;

            response.Result = null;
            response.Errors = new List<string>() { ex.Message, innerException.ToString() };
            response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            response.Result = null;
            response.Token = null;

            return new ObjectResult(response)
            {
                StatusCode = 500
            };
        }

    }
}
