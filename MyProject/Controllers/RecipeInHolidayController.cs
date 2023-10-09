using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Controllers
{

    // לעניות דעתי כדאי להפריד בין פעולות של מתכון לבין פעולות של מתכון בחג
    // על מנת שלא נידרש לעדכן את המתכון בכל פעם שנרצה להוסיף לו מועד שבו הוכן
    // כי במתכון יש המון שדות וכן שדות ששייכים לטבלאות אחרות
    // ולכן פעולת עדכון המתכון אורכת זמן רב


    [ApiController]
    [Route("api/[controller]")]
    public class RecipeInHolidayController : ControllerBase
    {
        private readonly RecipesDbContext _context;

        public RecipeInHolidayController(RecipesDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeInHoliday>>> GetRecipesInHolidays()
        {
            return await _context.recipeInHoliday.ToListAsync();
        }


        [HttpPut()]
        public async Task<IActionResult> PutRecipeInHoliday(int id, RecipeInHoliday recipeInHoliday)
        {
            if (id != recipeInHoliday.HolidayId)
            {
                return BadRequest();
            }
            _context.recipeInHoliday.Update(recipeInHoliday);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }


        // get the holidays of specific recipe
        [HttpGet()]
        [Route("one")]
        public async Task<ActionResult<RecipeInHoliday>> GetRecipeInHoliday(int id)
        {
            var recipeInHoliday = await _context.recipeInHoliday.FirstAsync(e => e.RecipeId == id);

            if (recipeInHoliday == null)
            {
                return NotFound();
            }

            return recipeInHoliday;
        }


        [HttpPost]
        public async Task<ActionResult<RecipeInHoliday>> PostRecipeInHoliday(RecipeInHoliday recipeInHoliday)
        {
            _context.recipeInHoliday.Add(recipeInHoliday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeInHoliday", new { id = recipeInHoliday.HolidayId }, recipeInHoliday);
        }

        [HttpDelete()]
        public async Task<ActionResult<RecipeInHoliday>> DeleteRecipeInHoliday(int id)
        {
            var recipeIn = await _context.recipeInHoliday.FirstAsync(e => e.HolidayId == id);

            if (recipeIn == null)
            {
                return NotFound();
            }

            _context.recipeInHoliday.Remove(recipeIn);
            await _context.SaveChangesAsync();

            return recipeIn;
        }


    }
}
