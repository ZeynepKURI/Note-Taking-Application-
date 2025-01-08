
using Application.DTOs;
using Application.Interfaces;


namespace Persistence.Repository
{
    public class NotesRepo : INotesRepo
    {
        public Task AddNotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotesAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<NoteDTO> GetAllByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<NoteDTO>> GetAllNotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NoteDTO> GetNotesByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNotesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

