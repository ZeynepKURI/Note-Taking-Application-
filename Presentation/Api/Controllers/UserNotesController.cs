using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
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

        public async Task<IActionResult> UpdateNotes()

        [HttpGet]
        public async Task<ActionResult<List<NoteDTO>>> Getnotes(int UserId)

        {

            try
            {
                var userId =await _notesService.GetNotesByUserIdAsync(UserId);
                if(userId!=null)
                {
                    return Ok(userId);

                }

                return Ok(" ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }
    }
}
