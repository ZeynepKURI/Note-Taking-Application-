using Application.DTOs;
using Application.Interfaces;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Persistence.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        // Admin için register metodu
        public async Task<RegisterResponse> RegisterAdminAsync(RegisterDTO registerDTO)
        {
            var existingAdmin = await FindAdminByEmail(registerDTO.Email);
            if (existingAdmin != null)
            {
                return new RegisterResponse(false, "Admin already exists");
            }

            var newAdmin = new Admin
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            _appDbContext.admins.Add(newAdmin);
            await _appDbContext.SaveChangesAsync();

            return new RegisterResponse(true, "Admin registered successfully");
        }

        // Admin için login metodu
        public async Task<LoginResponse> LoginAdminAsync(LoginDTO loginDTO)
        {
            var admin = await FindAdminByEmail(loginDTO.Email);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, admin.Password))
            {
                return new LoginResponse(false, "Invalid credentials");
            }

            var token = GenerateJWTToken(admin, "Admin");
            return new LoginResponse(true, token);
        }

        // User için register metodu
        public async Task<RegisterResponse> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var existingUser = await FindUserByEmail(registerDTO.Email);
            if (existingUser != null)
            {
                return new RegisterResponse(false, "User already exists");
            }

            var newUser = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            _appDbContext.users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return new RegisterResponse(true, "User registered successfully");
        }

        // User için login metodu
        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var user = await FindUserByEmail(loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                return new LoginResponse(false, "Invalid credentials");
            }

            var token = GenerateJWTToken(user, "User");
            return new LoginResponse(true, token);
        }

        // Helper method for finding admin by email
        private async Task<Admin> FindAdminByEmail(string email)
        {
            return await _appDbContext.admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        // Helper method for finding user by email
        private async Task<User> FindUserByEmail(string email)
        {
            return await _appDbContext.users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // JWT Token generation (both for Admin and User)
        private string GenerateJWTToken(dynamic user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role) // Add role information here
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
