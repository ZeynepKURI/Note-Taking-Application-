using System;
using Application.DTOs;

namespace Persistence.Repository
{
	public interface INotesRepo
	{
		Task<NoteDTO> GetNotesByIdAsync(int id);
		Task<List<NoteDTO>> GetAllNotesAsync();
		Task AddNotesAsync(NoteDTO noteDTO);
		Task UpdateNotesAsync(NoteDTO noteDTO);
		Task DeleteNotesAsync(int Id);
	}
}

