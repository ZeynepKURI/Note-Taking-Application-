using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface IAuthService
	{
		Task<RegisterResponse> RegisterAsync(RegisterDTO registerDTO);
		Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
	}
}

