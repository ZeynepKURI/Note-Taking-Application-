
using Application.DTOs;
using Application.Interfaces;
using Domain.Enitities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    public class NotesRepo : INotesRepo
    {

        private readonly AppDbContext context;
        public NotesRepo(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<List<NoteDTO>> GetAllNotesAsync()
        {
            var Notes = await context.notes.AsNoTracking()
                 .ToListAsync();

            return Notes.Select(n => new NoteDTO
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAt = n.CreatedAt,
            })
            .ToList();

        }
        //Neden AsNoTracking Kullanılır?
        //Performans: İzleme yapılmadığı için sorgu daha hızlı çalışır.
        //Bellek Yönetimi: DbContext izleme yapmadığından, bellekte daha az veri saklanır.
        //Sadece Okuma İşlemleri: Veri güncellenmeyecek veya silinmeyecekse, izleme gereksizdir.

        public async Task<NoteDTO> GetAllByIdAsync(int Id)
        {
            var Notes = await context.notes.AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id==Id);

            if (Notes == null)
                throw new Exception("Notes not found");

            return new NoteDTO
            {
                Id = Notes.Id,
                Title = Notes.Title,
                Content = Notes.Content,
                CreatedAt = Notes.CreatedAt
            };

        }

        public async Task AddNotesAsync(NoteDTO noteDTO)
        {
            var Notes = new Note
            {
                Id = noteDTO.Id,
                Title = noteDTO.Title,
                Content = noteDTO.Content,
                CreatedAt = noteDTO.CreatedAt
            };
            await context.notes.AddAsync(Notes);
            await context.SaveChangesAsync();
        }

        public async Task DeleteNotesAsync(int Id)
        {
            var Notes = await context.notes.FindAsync(Id);
            if( Notes!= null)
            {
                context.notes.Remove(Notes);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

   

        public async Task UpdateNotesAsync(NoteDTO noteDTO)
        {
            var Notes = await context.notes.FindAsync(noteDTO);
            if(Notes!=null)
            {
                Notes.Title = noteDTO.Title;
                Notes.Id = noteDTO.Id;
                Notes.Content = noteDTO.Content;
                Notes.CreatedAt = noteDTO.CreatedAt;

                context.notes.Update(Notes);
                await context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Note not found");
            }

            }

    
    }
}

