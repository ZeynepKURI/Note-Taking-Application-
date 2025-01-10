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

        public UserNotesController(INotesService notesService, IHttpContextAccessor httpContextAccessor)
        {
            _notesService = notesService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Get all notes for the logged-in user
        [HttpGet]
        public async Task<ActionResult<List<NoteDTO>>> GetUserNotes()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized("User ID not found or invalid");
                }

                var notes = await _notesService.GetNotesByUserIdAsync(userId);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get a specific note by ID for the logged-in user
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDTO>> GetAllByIdNotes(int id)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized("User ID not found or invalid");
                }

                var note = await _notesService.GetNotesByIdAsync(id);

                if (note == null)
                {
                    return NotFound("Note not found");
                }

                if (note.UserId != userId)
                {
                    return Forbid("You are not the author of this note.");
                }

                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Create a new note for the logged-in user
        [HttpPost]
        public async Task<ActionResult> CreateNotes([FromBody] NoteDTO noteDTO)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized("User ID not found or invalid");
                }

                noteDTO.UserId = userId;  // Ensure the note is created with the logged-in user ID
                await _notesService.AddNotesAsync(noteDTO);
                return Ok("Note created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
