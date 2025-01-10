
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // Get all notes
        [HttpGet]
        [Authorize]

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

        // Get a note by Id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NoteDTO>> GetAllNotesById([FromQuery] int Id)
        {
            try
            {
                var notes = await _noteService.GetNotesByIdAsync(Id);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update note
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNote(int Id, [FromBody] NoteDTO noteDTO)
        {
            try
            {
                await _noteService.UpdateNotesAsync(noteDTO, Id);
                return Ok("Note updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Add note
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddNotes([FromBody] NoteDTO noteDTO)
        {
            try
            {
                await _noteService.AddNotesAsync(noteDTO);
                return Ok("Note added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Bir notu silen endpoint
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteNotes(int Id)
        {
            try
            {
                await _noteService.DeleteNotesAsync(Id);
                return Ok("Note deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}