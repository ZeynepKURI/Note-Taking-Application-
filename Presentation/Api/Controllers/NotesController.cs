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
        [Authorize(Roles = "Admin")]// Kimlik doğrulaması yapılmış kullanıcılar erişebilir
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
        public async Task<ActionResult<NoteDTO>> GetAllNotesById([FromRoute] int id) //[FromRoute], route'dan gelen parametreleri açık bir şekilde metot parametrelerine bağlamanızı sağlar. 
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

       
    }
}
