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

//interface'ler (IService ve IRepository) genellikle domain veya core katmanında yer alır. Bu sayede bağımlılık tersine çevrilir (Dependency Inversion Principle), ve uygulamanın diğer katmanları (örneğin, Infrastructure veya Application) bu interface'leri implement ederek iş mantığını sağlar.

