﻿using System.Security.Cryptography;

namespace RecipeHubAPI.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password, out byte[] salt);
        bool VerifyPassword(string password, string hash, byte[] salt);
    }
}
