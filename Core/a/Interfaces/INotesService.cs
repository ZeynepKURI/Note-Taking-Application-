using System;
using Application.DTOs;
using Domain.Enitities;

namespace Application.Interfaces
{
	public interface INotesService
	{
        Task<List<NoteDTO>> GetAllNotesAsync();
        Task<NoteDTO> GetNotesByIdAsync(int Id);
        Task AddNotesAsync(NoteDTO noteDTO);
        Task UpdateNotesAsync(NoteDTO noteDTO, int Id);
        Task DeleteNotesAsync(int Id);
        Task<List<NoteDTO>> GetNotesByUserIdAsync(int userId);  // Kullanıcıya öz

    }
}

//interface'ler (IService ve IRepository) genellikle domain veya core katmanında yer alır. Bu sayede bağımlılık tersine çevrilir (Dependency Inversion Principle), ve uygulamanın diğer katmanları (örneğin, Infrastructure veya Application) bu interface'leri implement ederek iş mantığını sağlar.

