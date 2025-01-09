
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Enitities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence.Service
{

    //Service, iş mantığını ve veritabanı işlemlerini koordine eden katmandır.


    public class NotesService : INotesService
    {
        private readonly INotesRepo _notesRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotesService(INotesRepo  notesRepo, IMapper mapper , IHttpContextAccessor httpContextAccessor)
        {
            _notesRepo = notesRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<NoteDTO>> GetAllNotesAsync()
        {
            // Kullanıcı ID'sini ve rolünü al
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
            var roles = _httpContextAccessor.HttpContext.User.FindFirst("role")?.Value;

            if (roles == "Admin")
            {
                // Admin ise tüm notları getir ve DTO'ya dönüştür
                var notes = await _notesRepo.GetAllNotesAsync();
                return _mapper.Map<List<NoteDTO>>(notes); // Entity'leri DTO'ya dönüştür
            }
            else
            {
                // Kullanıcı ise sadece kendi notlarını getir ve DTO'ya dönüştür
                var notes = await _notesRepo.GetNotesByUserIdAsync(userId);
                return _mapper.Map<List<NoteDTO>>(notes); // Entity'leri DTO'ya dönüştür
            }
        }




        public async Task<NoteDTO> GetNotesByIdAsync(int Id)
        {
            var Notes = await _notesRepo.GetAllByIdAsync(Id);

            if(Notes!=null)
            {
                return _mapper.Map< NoteDTO > (Notes);
            }

            else
            {
                throw new Exception("Notes not found");
            }

        }


        public async Task AddNotesAsync(NoteDTO noteDTO)
        {
            // DTO'yu Entity'ye dönüştürmek için AutoMapper kullanabilirsiniz (ya da manuel yapabilirsiniz)
            var Notes = _mapper.Map<Note>(noteDTO);

            // Repository üzerinden ekleme işlemi
            await _notesRepo.AddNotesAsync(Notes);
        }


        public async Task DeleteNotesAsync(int Id)
        {
            var Notes = await _notesRepo.GetAllByIdAsync(Id);
            if(Notes!=null)
            {
                await _notesRepo.DeleteNotesAsync(Id);
               
            }
            else
            {
                throw new Exception("Notes not found");
            }
        }

       
     

        public async Task UpdateNotesAsync(NoteDTO noteDTO, int Id)
        {
            var Notes = await _notesRepo.GetAllByIdAsync(Id);
            if( Notes!=null)
            {
                Notes.Title = noteDTO.Title;
                Notes.Content = noteDTO.Content;
                Notes.CreatedAt = noteDTO.CreatedAt;

                await _notesRepo.UpdateNotesAsync(Notes);
                
            }
        }

     
      
    }
}

