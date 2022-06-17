using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApi.Data;
using NoteApi.Model.Entities;

namespace NoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        public readonly NoteDbContext noteDbContext;
        public NoteController(NoteDbContext noteDbContext)
        {
            this.noteDbContext = noteDbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await noteDbContext.Notes.ToListAsync());
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNotesByID")]
        public async Task<IActionResult> GetNotesById([FromRoute] Guid id)
        {
            var note = await noteDbContext.Notes.FindAsync(id);

            if(note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            await noteDbContext.Notes.AddAsync(note);
            await noteDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotesById), new {id =note.Id} ,note);

        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note updatedNote)
        {
            var existingNote = await noteDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }
            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.IsVisible = updatedNote.IsVisible;

            await noteDbContext.SaveChangesAsync();
            return Ok(existingNote);


        }



        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id )
        {
            var existingNote = await noteDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }
            noteDbContext.Notes.Remove(existingNote);
            await noteDbContext.SaveChangesAsync();
            return Ok();
           


        }


    }

}
