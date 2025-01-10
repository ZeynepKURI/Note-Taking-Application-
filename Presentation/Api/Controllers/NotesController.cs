
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/controller")]  // Controller için route tanımlaması
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _noteService;

        public NotesController(INotesService noteService)
        {
            _noteService = noteService;
        }



        [HttpGet]
        [Authorize]

        public async Task<ActionResult> GetAllNotes()
        {
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
        
        }



        [HttpGet("id")]
        [Authorize]

        public async Task<ActionResult<NoteDTO>> GetAllNotesById(int Id)
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



        [HttpPut("id")]
        public async Task<ActionResult> UpdateNote(int Id, [FromBody] NoteDTO noteDTO)
        {
            try
            {
                await _noteService.UpdateNotesAsync(noteDTO, Id);
                return Ok("Note updated succesfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddNotes([FromBody] NoteDTO noteDTO)
        {
            try
            {
                await _noteService.AddNotesAsync(noteDTO);
                return Ok("Note Added succesfully.");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult>DeleteNotes(int Id)
        {
            try
            {
                await _noteService.DeleteNotesAsync(Id);
                return Ok("Note Delete Succesfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}