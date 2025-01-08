
using Application.DTOs;
using Application.Interfaces;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    public class NotesRepo : INotesRepo
    {


        //Repository, veritabanına erişim sağlayan katmandır.entity kullanılır.

        private readonly AppDbContext context;
        public NotesRepo(AppDbContext _context)
        {
            context = _context;
        }

        public Task AddNotesAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotesAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetAllByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> GetAllNotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateNotesAsync(Note note)
        {
            throw new NotImplementedException();
        }
    }
}

