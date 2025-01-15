
using System.IdentityModel.Tokens.Jwt;

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



        // Tüm notları getir
        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await context.notes.ToListAsync();
        }

        // ID'ye göre bir notu getir
        public async Task<Note> GetAllByIdAsync(int Id)
        {
            return await context.notes.FirstOrDefaultAsync(n => n.Id == Id);
        }


        // Kullanıcıya ait notları getir
        public async Task<List<Note>> GetNotesByUserIdAsync(int userId)
        {
            return await context.notes.Where(n => n.UserId == userId).ToListAsync();
        }



        // Yeni not ekle
        public async Task AddNotesAsync(Note note)
        {
            await context.notes.AddAsync(note);
            await context.SaveChangesAsync();
        }



        // Not sil
        public async Task DeleteNotesAsync(int Id)
        {
            var note = await context.notes.FindAsync(Id);
            if(note!=null)
            {
                context.notes.Remove(note);
                await context.SaveChangesAsync();
            }
        }

        // Not güncelle
        public async Task UpdateNotesAsync(Note note)
        {
           context.notes.Update(note)
                ;
            await context.SaveChangesAsync();
        }

      
    }
}

