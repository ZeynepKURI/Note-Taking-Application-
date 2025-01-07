using System;
using Application.DTOs;
using Persistence.Service;

namespace Persistence.Repository
{
    public class UserRepo : IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}

