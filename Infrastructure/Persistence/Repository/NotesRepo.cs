using System;
using Application.DTOs;
using Persistence.Service;

namespace Persistence.Repository
{
    public class NotesRepo : INotesService
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

