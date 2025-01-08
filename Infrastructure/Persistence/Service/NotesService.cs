
using Application.DTOs;
using Application.Interfaces;
using Persistence.Context;

namespace Persistence.Service
{
    public class NotesService : INotesService
    {
        private readonly INotesRepo _notesRepo;

        public NotesService(INotesRepo  notesRepo)
        {
            _notesRepo = notesRepo;
        }




        public Task AddNotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotesAsync(int Id)
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

