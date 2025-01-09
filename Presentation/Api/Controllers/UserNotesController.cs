using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/user")]
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }




        }









    }

}