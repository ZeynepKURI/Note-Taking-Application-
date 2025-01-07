using System;
using Application.DTOs;

namespace Persistence.Service
{
	public interface INotesService
	{
        Task<List<NoteDTO>> GetAllNotesAsync();
        Task<NoteDTO> GetNotesByIdAsync(int Id);
        Task AddNotesAsync();
        Task UpdateNotesAsync();
        Task DeleteNotesAsync(int Id);

    }
}

