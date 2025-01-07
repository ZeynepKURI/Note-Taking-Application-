using System;
using Application.DTOs;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;
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

        private async Task<Admin> FindUserByEmail(string email) =>
            await appDbContext.admins.FirstOrDefaultAsync(u => u.Email == email);
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

        public async Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO)
        {
            var getUser = await FindUserByEmail(registerDTO.Email!);
            if (getUser != null)
                return new RegisterResponse(false, "User already exists");

            appDbContext.admins.Add(new Admin()
            {
                Username = registerDTO.Username,
                Email= registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
           

            });
            await appDbContext.SaveChangesAsync();
            return new RegisterResponse(true, "User registered succesfully");


        }
    }
}

