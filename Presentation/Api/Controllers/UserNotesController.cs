using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Service;


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

        [Authorize(Roles = "User")]
        public async Task<ActionResult> CreateNotes([FromBody] NoteDTO noteDTO)
        {
            try
            {
                

       
                await _notesService.AddNotesAsync(noteDTO);
                return Ok("Note created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> UpdateNotes(int Id, [FromBody] NoteDTO noteDTO)
        {
            try
            {
           
                await _notesService.UpdateNotesAsync(noteDTO,Id);

                return Ok("Note updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(int Id, int UserId)
        {
            try
            {
             

                // Servis katmanında kontrol yapıyoruz
                await _notesService.DeleteNotesAsync(Id, UserId);

                return NoContent();  // Silme işlemi başarılı, geri dönüş yok
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You can only delete your own notes.");  // Yetkisiz erişim hatası
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Diğer hatalar
            }
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<NoteDTO>>> GetNotes(int UserId)

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
