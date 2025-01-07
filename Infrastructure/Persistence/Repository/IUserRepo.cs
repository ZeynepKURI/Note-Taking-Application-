using System;
using Application.DTOs;

namespace Persistence.Repository
{
	public interface IUserRepo
	{
        Task<LoginDTO> GetUserByIdAsync(int Id);
        Task<IEnumerable<LoginDTO>> GetAllUsersAsync();
        Task AddUserAsync(LoginDTO loginDTO);
        Task UpdateUserAsync(LoginDTO loginDTO);
        Task DeleteUserAsync(int Id);
    }
}

