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
            var getAdmin = await FindAdminByEmail(loginDTO.Email!);
            if (getAdmin == null)
                return new LoginResponse(false, "Admin not found");

            // Şifreyi doğrulayalım
            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getAdmin.Password);
            if (checkPassword)
                return new LoginResponse(true, "Login successful", GenerateJWTToken(getAdmin));
            else
                return new LoginResponse(false, "Invalid credentials");
        }


        private async Task<Admin> FindAdminByEmail(string email) =>
       await appDbContext.admins.FirstOrDefaultAsync(u => u.Email == email);

        private string GenerateJWTToken(Admin admins)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, admins.Id.ToString()),
                new Claim(ClaimTypes.Name, admins.Username!),
                new Claim(ClaimTypes.Email, admins.Email!)
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
            var getAdmin= await FindAdminByEmail(registerDTO.Email!);
            if (getAdmin!= null)
                return new RegisterResponse(false, "User already exists");

            appDbContext.admins.Add(new Admin()
            {
                Username = registerDTO.Username,
                Email= registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
           

            });
            await appDbContext.SaveChangesAsync();
            return new RegisterResponse(true, "Admin registered succesfully");


        }
    }
}

