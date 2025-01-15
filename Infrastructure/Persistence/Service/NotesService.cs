using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Enitities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Persistence.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Persistence.Service
{
    // Service katmanı iş mantığını ve veritabanı işlemlerini koordine eden katmandır.
    public class NotesService : INotesService
    {
        private readonly INotesRepo _notesRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public NotesService(INotesRepo notesRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _notesRepo = notesRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        // Kullanıcı bilgilerini JWT'den alıyoruz
        private int GetUserIdFromJwt()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token is missing");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                throw new UnauthorizedAccessException("Invalid or missing user ID in token");

            return userId;
        }


        // Kullanıcının rolünü belirler
        private string GetUserRole()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst("role")?.Value;
        }





        // Tüm notları getiren metod (Admin için tüm notlar, kullanıcı için sadece kendi notları)
        public async Task<List<NoteDTO>> GetAllNotesAsync()
        {
            var userId = GetUserIdFromJwt();
            var roles = GetUserRole();

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
            var userId = GetUserIdFromJwt();
            var roles = GetUserRole();

            if (note != null)
            {
                // Admin değilse ve kullanıcı kendi notuna erişmeye çalışıyorsa, yetkilendirme kontrolü yapılır
                if (roles != "Admin" && note.UserId != userId)
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
            var userId = GetUserIdFromJwt();

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User ID not found in JWT.");
            }

            // Not verisini domain modeline dönüştür
            var note = _mapper.Map<Note>(noteDTO);
            note.UserId = userId; // Kullanıcı kimliğini ata
            note.CreatedAt = DateTime.UtcNow; // Oluşturma tarihini ata
            note.UpdatedAt = null; // İlk oluşturulduğunda güncellenme tarihi yok

            // Notu veritabanına ekle
            await _notesRepo.AddNotesAsync(note);

        
        }



        public async Task DeleteNotesAsync(int Id)
        {
            var note = await _notesRepo.GetAllByIdAsync(Id);

            if (note == null)
            {
                throw new Exception("Note not found");
            }

            var userId = GetUserIdFromJwt();

          
            if (note.UserId != userId && GetUserRole() != "User")
            {
                await _notesRepo.DeleteNotesAsync(Id);
            }
            else
            {
                throw new UnauthorizedAccessException("You can only delete your own notes.");
            }
        }





        // Bir notu güncelleyen metod
        public async Task UpdateNotesAsync(NoteDTO noteDTO, int Id)
        {
            var note = await _notesRepo.GetAllByIdAsync(Id);

            if (note == null)
            {
                throw new Exception("Note not found");
            }

            var userId = GetUserIdFromJwt();

            
            if (note.UserId != userId && GetUserRole() != "User")

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






        // Kullanıcıya ait notları getiren metod
        public async Task<List<NoteDTO>> GetNotesByUserIdAsync(int Id)
        {
            var notes = await _notesRepo.GetNotesByUserIdAsync(Id);
            return _mapper.Map<List<NoteDTO>>(notes);
        }

      
      
    }
}
