using System;
using Application.DTOs;

namespace Persistence.Service
{
	public interface IAuthService
	{
		Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO);
		Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
	}
}

