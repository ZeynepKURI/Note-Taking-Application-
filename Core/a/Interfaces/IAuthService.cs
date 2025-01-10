using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface IAuthService
	{

        // Admin için register ve login metodları
        Task<RegisterResponse> RegisterAdminAsync(RegisterDTO registerDTO);
        Task<LoginResponse> LoginAdminAsync(LoginDTO loginDTO);

        // User için register ve login metodları
        Task<RegisterResponse> RegisterUserAsync(RegisterDTO registerDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);


    }
}

//interface'ler (IService ve IRepository) genellikle domain veya core katmanında yer alır. Bu sayede bağımlılık tersine çevrilir (Dependency Inversion Principle), ve uygulamanın diğer katmanları (örneğin, Infrastructure veya Application) bu interface'leri implement ederek iş mantığını sağlar.

