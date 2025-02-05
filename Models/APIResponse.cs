﻿using System.Net;

namespace RecipeHubAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode? StatusCode { get; set; }
        public bool? IsSuccess { get; set; } = true;
        public List<String>? Errors { get; set; }
        public object? Result { get; set; }
        public string? Token { get; set; }
    }
}
