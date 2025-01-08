using System;
using Application.DTOs;

namespace Application.Interfaces
{
	public interface INotesRepo
	{

		Task<List<NoteDTO>> GetAllNotesAsync();
		Task<NoteDTO> GetAllByIdAsync(int Id);
		Task AddNotesAsync(NoteDTO noteDTO);
		Task UpdateNotesAsync(NoteDTO noteDTO);
		Task DeleteNotesAsync(int Id);

	}
}

