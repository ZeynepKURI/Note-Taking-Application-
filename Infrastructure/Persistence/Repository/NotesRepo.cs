
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

        public async Task AddNotesAsync(Note note)
        {
            await context.notes.AddAsync(note);
            await context.SaveChangesAsync();
        }
           

        public async Task DeleteNotesAsync(int Id)
        {
            var note = await context.notes.FindAsync(Id);
            if(note!=null)
            {
                context.notes.Remove(note);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Note> GetAllByIdAsync(int Id)
        {
            return await context.notes.FindAsync(Id);
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await context.notes.ToListAsync();
        }

        public async Task UpdateNotesAsync(Note note)
        {
           context.notes.Update(note);
            await context.SaveChangesAsync();
        }


        public async Task<List<Note>> GetNotesByUserIdAsync(int userId)
        {
            return await context.notes
                .Where(n => n.UserId == userId)
               .ToListAsync();
        }

        public Task<List<Note>> GetNotesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

