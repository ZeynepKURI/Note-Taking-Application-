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
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
            var roles = _httpContextAccessor.HttpContext.User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User ID is missing or invalid.");
            }

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
                var userId = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
                var roles = _httpContextAccessor.HttpContext.User.FindFirst("role")?.Value;

                // Admin değilse ve kullanıcı kendi notuna erişmeye çalışıyorsa, yetkilendirme kontrolü yapılır
                if (roles != "Admin" && note.UserId != int.Parse(userId))
                {
                    throw new UnauthorizedAccessException("You can only access your own notes.");
                }

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
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User ID is invalid.");
            }

            var note = _mapper.Map<Note>(noteDTO);
            note.UserId = userId; // Yeni notu kullanıcıya ata
            await _notesRepo.AddNotesAsync(note);
        }

        // Bir notu silen metod
        public async Task DeleteNotesAsync(int id)
        {
            var note = await _notesRepo.GetAllByIdAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found");
            }

            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User ID is missing or invalid.");
            }

            // Kullanıcı kendi notunu silebilir veya admin silme yetkisine sahip olmalı
            if (note.UserId == userId || _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                await _notesRepo.DeleteNotesAsync(id);
            }
            else
            {
                throw new UnauthorizedAccessException("You can only delete your own notes.");
            }
        }

        // Bir notu güncelleyen metod
        public async Task UpdateNotesAsync(NoteDTO noteDTO, int id)
        {
            var note = await _notesRepo.GetAllByIdAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found");
            }

            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User ID is missing or invalid.");
            }

            // Kullanıcı kendi notunu güncelleyebilir veya admin güncelleyebilir
            if (note.UserId == userId || _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                note.Title = noteDTO.Title;
                note.Content = noteDTO.Content;
                note.CreatedAt = noteDTO.CreatedAt;

                await _notesRepo.UpdateNotesAsync(note);
            }
            else
            {
                throw new UnauthorizedAccessException("You can only update your own notes.");
            }
        }

        // Kullanıcıya ait notları getiren metod (yeni eklenen metod)
        public async Task<List<NoteDTO>> GetNotesByUserIdAsync(int userId)
        {
            var notes = await _notesRepo.GetNotesByUserIdAsync(userId);
            return _mapper.Map<List<NoteDTO>>(notes);
        }
    }
}
