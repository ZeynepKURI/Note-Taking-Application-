using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.Interfaces;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;

namespace Persistence.Repository
{
    public class UserRepo : IAuthService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public UserRepo(AppDbContext appDbContext, IConfiguration configuration)
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
                return new LoginResponse(true, "Login successful", GenerateJWTToken(getUser));
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        private async Task<User> FindUserByEmail(string email) =>
            await appDbContext.users.FirstOrDefaultAsync(u => u.Email == email);

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO)
        {
            var getUser = await FindUserByEmail(registerDTO.Email!);
            if (getUser != null)
                return new RegisterResponse(false, "User already exists");

            appDbContext.users.Add(new User()
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
            });
            await appDbContext.SaveChangesAsync();
            return new RegisterResponse(true, "User registered successfully");
        }
    }
}
