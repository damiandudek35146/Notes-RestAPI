using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesOrganizer.Core.Domain;
using NotesOrganizer.Infrastructure.DTO;
using NotesOrganizer.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var notes = _noteService.GetAll();
            return Ok(notes);
        }

        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
            var note = _noteService.Get(id);
            if (note == null)
            {
                return BadRequest();
            }
            return Ok(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NoteDto note)
        {
            var guid = Guid.NewGuid();
            note = _noteService.Created(guid, note.Title, note.Content);
            return Created($"api/notes/{guid}", note);
        }

        [HttpPut("id")]
        public IActionResult Put(Guid id, [FromBody] NoteDto note)
        {
            _noteService.Update(id, note.Title, note.Content);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Delete(Guid id)
        {
            _noteService.Delete(id);
            return NoContent();
        }
    }
}
