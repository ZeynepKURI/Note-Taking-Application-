
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Enitities;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence.Service
{

    //Service, iş mantığını ve veritabanı işlemlerini koordine eden katmandır.


    public class NotesService : INotesService
    {
        private readonly INotesRepo _notesRepo;
        private readonly IMapper _mapper;

        public NotesService(INotesRepo  notesRepo, IMapper mapper)
        {
            _notesRepo = notesRepo;
            _mapper = mapper;
        }


        public async Task<List<NoteDTO>> GetAllNotesAsync()
        {
            var Notes = await _notesRepo.GetAllNotesAsync();

            // Entity'leri DTO'ya dönüştürme
            return _mapper.Map<List<NoteDTO>>(Notes);
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

