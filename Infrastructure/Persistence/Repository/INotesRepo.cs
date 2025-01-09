using System;
using Domain.Enitities;


namespace Persistence.Repository
{
	public interface INotesRepo
	{

		Task<List<Note>> GetAllNotesAsync();
		Task<Note> GetAllByIdAsync(int Id);
		Task AddNotesAsync(Note note);
		Task UpdateNotesAsync(Note note);
		Task DeleteNotesAsync(int Id);
        Task<List<Note>> GetNotesByUserIdAsync(int userId);  // Kullanıcıya özel notları getiren metot

    }
}

