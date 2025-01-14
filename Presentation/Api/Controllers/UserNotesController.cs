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

        [HttpPut("{UserId}")]
        public async Task<ActionResult> UpdateNotes(int Id, [FromBody] NoteDTO noteDTO)
        {
            try
            {
                // Kullanıcı kimliğini doğrula
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized("User ID not found or invalid");
                }

                // Notun var olup olmadığını ve bu kullanıcıya ait olup olmadığını kontrol et
                var existingNote = await _notesService.GetNotesByIdAsync(Id);
                if (existingNote == null)
                {
                    return NotFound("Note not found");
                }

                if (existingNote.UserId!= userId)
                {
                    return Forbid("You are not authorized to update this note");
                }

                // Güncellemeyi gerçekleştir
                noteDTO.Id= Id; // Güncellenen notun ID'sini ayarla
                noteDTO.UserId = userId; // Kullanıcı ID'sini sabitle
                await _notesService.UpdateNotesAsync(noteDTO,Id);

                return Ok("Note updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteAsync(int Id)
        {
            try
            {
                await _notesService.DeleteNotesAsync(Id);

                    return Ok("Delete succesfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            

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
