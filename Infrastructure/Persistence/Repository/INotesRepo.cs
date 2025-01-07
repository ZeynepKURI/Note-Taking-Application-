using System;
using Application.DTOs;

namespace Persistence.Repository
{
	public interface INotesRepo
	{
		Task<List<NoteDTO>> GetAllNotesAsync();
		Task<NoteDTO> GetNoteByIdAsync(int id);
		Task AddNotesAsync(NoteDTO noteDTO);
		Task UpdateNotesAsync(NoteDTO noteDTO);
		Task DeleteNotesAsync(int Id);
	}
}

