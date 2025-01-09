
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/user/notes")]
    [ApiController]
    public class UserNotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserNotesController( INotesService notesService, IHttpContextAccessor httpContextAccessor)
        {
            _notesService = notesService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<ActionResult<List<NoteDTO>>> GetUserNotes()

        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(" User Id not found");
                }

                var notes = await _notesService.GetNotesByUserIdAsync(userId);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  

        }

        [HttpGet("id")]
        public async Task<ActionResult<NoteDTO>> GetAllByIdNotes(int Id)
        {

            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if(string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {

                    return Unauthorized("User Id not found");
                }
                

                var Notes = await _notesService.GetNotesByIdAsync(Id);

                if(Notes.UserId != userId)
                {
                    return Forbid("You are not author. ");
                }

                return Ok(Notes);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPost]
        public async Task<ActionResult> CreateNotes([FromBody] NoteDTO noteDTO)
        {

            try
            { var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized("User ID not found or invalid");
                }
                await _notesService.AddNotesAsync(noteDTO);
                return Ok("Note created successfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }

}