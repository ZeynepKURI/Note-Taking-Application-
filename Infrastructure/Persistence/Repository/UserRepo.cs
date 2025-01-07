using System;
using Application.DTOs;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using Persistence.Service;

namespace Persistence.Repository
{
    public class UserRepo : IAuthService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public UserRepo(AppDbContext appDbContext,IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }
        private async Task<User> FindUserAsyncEmail(string email)=>
            await appDbContext.users.FirstOrDefaultAsync(u=>u.Email==email);
       
        public async Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserAsyncEmail(loginDTO.Email!);
            if (getUser == null)
                return new LoginResponse(false, "User not found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (checkPassword)
                return new LoginResponse(true, "Login Successful");
            else 
                return new LoginResponse(true, "Invalid credentials");
        }



        public async Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO)
        {
            var getUser = await FindUserAsyncEmail(registerDTO.Email!);
            if (getUser != null)
                return new RegisterResponse(false, "User already exists");

            appDbContext.users.Add(new User()
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            });

            await appDbContext.SaveChangesAsync();
            return new RegisterResponse(true, "User registered successfully");
        }
    }
}

