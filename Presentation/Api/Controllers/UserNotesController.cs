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


        [HttpGet("notes")]
        public async Task<ActionResult<List<NoteDTO>>> GetUserNotes()

        {
            try
            {

                var Notes = await _notesService.GetAllNotesAsync();
                return Ok(Notes);

            }
            catch(UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }

            catch ( Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }









    }

}