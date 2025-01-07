using System;
using Application.DTOs;
using Domain.Enitities;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using Persistence.Service;

namespace Persistence.Repository
{
   
    public class AdminRepo : IAuthService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public AdminRepo( AppDbContext appDbContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.appDbContext = appDbContext;
        }
      

        public async Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
        {
            // Kullanıcıyı e-posta ile bulalım
            var getUser = await FindUserByEmail(loginDTO.Email!);
            if (getUser == null)
                return new LoginResponse(false, "User not found");

            // Şifreyi doğrulayalım
            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (checkPassword)
                return new LoginResponse(true, "Login successful");
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        public Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}

