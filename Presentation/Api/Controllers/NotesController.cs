
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/admin")]  // Controller için route tanımlaması
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _noteService;

        public NotesController(INotesService noteService)
        {
            _noteService = noteService;
        }

        
        [HttpGet]
        public async Task<ActionResult> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();
            return Ok(notes);
        }

    

       
    }
}