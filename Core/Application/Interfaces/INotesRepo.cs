using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface INotesRepo
	{

		Task<List<NoteDTO>> GetAllNotesAsync();
		Task<NoteDTO> GetAllByIdAsync(int Id);
		Task AddNotesAsync();
		Task UpdateNotesAsync();
		Task DeleteNotesAsync(int Id);

	}
}

