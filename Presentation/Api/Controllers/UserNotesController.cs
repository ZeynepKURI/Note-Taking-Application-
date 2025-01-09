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

        public UserNotesController( INotesService notesService)
        {
            _notesService = notesService;
        }


        [HttpGet("notes")]
        public async Task<ActionResult<List<NoteDTO>>> GetUserNotes(NoteDTO noteDTO)

        {
            var userId = User.FindFirst("sub")?.Value;

            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User Id not found");
            }

            var Notes = await _notesService.GetNotesByIdAsync(userId);
            return Ok(Notes);
        }
           



    }

}