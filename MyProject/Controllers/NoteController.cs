using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly RecipesDbContext _context;

        public NoteController(RecipesDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Note.ToListAsync();
        }


        [HttpPut()]
        public async Task<IActionResult> PutNote(int num, Note note)
        {
            if (num != note.NoteNumber)
            {
                return BadRequest();
            }
            _context.Note.Update(note);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpGet()]
        [Route("one-recipe")]
        public async Task<ActionResult<List<Note>>> GetNote(int recipeId)
        {

            var notes = _context.Note.ToListAsync().Result.FindAll(e => e.RecipeId == recipeId);

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }


        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.RecipeId }, note);
        }

        [HttpDelete()]
        public async Task<ActionResult<Note>> DeleteNote(int recipeId, int noteNum)
        {
            var note = await _context.Note.FirstAsync(e => e.RecipeId == recipeId && e.NoteNumber == noteNum);

            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return note;
        }


    }
}
