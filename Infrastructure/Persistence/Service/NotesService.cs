using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Enitities;
using Microsoft.AspNetCore.Http;
using Persistence.Repository;


namespace Persistence.Service
{
    // Service katmanı iş mantığını ve veritabanı işlemlerini koordine eden katmandır.
    public class NotesService : INotesService
    {
        private readonly INotesRepo _notesRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotesService(INotesRepo notesRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _notesRepo = notesRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // Tüm notları getiren metod (Admin için tüm notlar, kullanıcı için sadece kendi notları)
        public async Task<List<NoteDTO>> GetAllNotesAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
            var roles = _httpContextAccessor.HttpContext.User.FindFirst("role")?.Value;

            // Admin için tüm notları getir
            if (roles == "Admin")
            {
                var notes = await _notesRepo.GetAllNotesAsync();
                return _mapper.Map<List<NoteDTO>>(notes);
            }
            else
            {
                // Kullanıcı için sadece kendi notlarını getir
                var notes = await _notesRepo.GetNotesByUserIdAsync(userId);
                return _mapper.Map<List<NoteDTO>>(notes);
            }
        }

        // ID'ye göre bir notu getiren metod
        public async Task<NoteDTO> GetNotesByIdAsync(int id)
        {
            var note = await _notesRepo.GetAllByIdAsync(id);

            if (note != null)
            {
                return _mapper.Map<NoteDTO>(note);
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

        // Yeni bir not ekleyen metod
        public async Task AddNotesAsync(NoteDTO noteDTO)
        {
            var note = _mapper.Map<Note>(noteDTO);
            await _notesRepo.AddNotesAsync(note);
        }

        // Bir notu silen metod
        public async Task DeleteNotesAsync(int id)
        {
            var note = await _notesRepo.GetAllByIdAsync(id);
            if (note != null)
            {
                await _notesRepo.DeleteNotesAsync(id);
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

        // Bir notu güncelleyen metod
        public async Task UpdateNotesAsync(NoteDTO noteDTO, int id)
        {
            var note = await _notesRepo.GetAllByIdAsync(id);
            if (note != null)
            {
                note.Title = noteDTO.Title;
                note.Content = noteDTO.Content;
                note.CreatedAt = noteDTO.CreatedAt;

                await _notesRepo.UpdateNotesAsync(note);
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

        // Kullanıcıya ait notları getiren metod (yeni eklenen metod)
        public async Task<List<NoteDTO>> GetNotesByUserIdAsync(string userId)
        {
            var notes = await _notesRepo.GetNotesByUserIdAsync(userId);
            return _mapper.Map<List<NoteDTO>>(notes);
        }
    }
}
