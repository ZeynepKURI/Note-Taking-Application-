using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _noteService;

        public NotesController(INotesService noteService)
        {
            _noteService = noteService;
        }

        // Tüm notları getir
        [HttpGet]
        [Authorize]  // Kimlik doğrulaması yapılmış kullanıcılar erişebilir
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                var notes = await _noteService.GetAllNotesAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ID'ye göre bir notu getir
        [HttpGet("{id}")]
        [Authorize]  // Kimlik doğrulaması yapılmış kullanıcılar erişebilir
        public async Task<ActionResult<NoteDTO>> GetAllNotesById([FromRoute] int id)
        {
            try
            {
                var notes = await _noteService.GetNotesByIdAsync(id);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Notu güncelle
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]  // Yalnızca Admin rolündeki kullanıcılar her notu güncelleyebilir
        public async Task<ActionResult> UpdateNote(int id, [FromBody] NoteDTO noteDTO)
        {
            try
            {
                await _noteService.UpdateNotesAsync(noteDTO, id);
                return Ok("Not başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Yeni bir not ekle
        [HttpPost]
        [Authorize]  // Yalnızca kimlik doğrulaması yapılmış kullanıcılar not ekleyebilir
        public async Task<ActionResult> AddNotes([FromBody] NoteDTO noteDTO)
        {
            try
            {
                await _noteService.AddNotesAsync(noteDTO);
                return Ok("Not başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Bir notu sil
        [HttpDelete("{id}")]
        [Authorize]  // Yalnızca kimlik doğrulaması yapılmış kullanıcılar not silebilir
        public async Task<ActionResult> DeleteNotes(int id)
        {
            try
            {
                await _noteService.DeleteNotesAsync(id);
                return Ok("Not başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
