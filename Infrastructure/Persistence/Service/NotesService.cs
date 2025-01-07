using System;
using Application.DTOs;

namespace Persistence.Service
{
    public class NotesService : INotesService
    {
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

