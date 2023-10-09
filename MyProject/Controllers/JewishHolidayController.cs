using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JewishHolidayController : ControllerBase
    {
        private readonly RecipesDbContext _context;

        public JewishHolidayController(RecipesDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<JewishHoliday>>> GetHolidays()
        {
            return await _context.jewishHoliday.ToListAsync();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, JewishHoliday jewishHoliday)
        {
            if (id != jewishHoliday.HolidayId)
            {
                return BadRequest();
            }
            _context.jewishHoliday.Update(jewishHoliday);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<JewishHoliday>> GetHoliday(int id)
        {
            var holiday = await _context.jewishHoliday.FirstAsync(e => e.HolidayId == id);

            if (holiday == null)
            {
                return NotFound();
            }

            return holiday;
        }


        [HttpPost]
        public async Task<ActionResult<JewishHoliday>> PostHoliday(JewishHoliday holiday)
        {
            _context.jewishHoliday.Add(holiday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoliday", new { id = holiday.HolidayId }, holiday);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<JewishHoliday>> DeleteHoliday(int id)
        {
            var holiday = await _context.jewishHoliday.FirstAsync(e => e.HolidayId == id);

            if (holiday == null)
            {
                return NotFound();
            }

            _context.jewishHoliday.Remove(holiday);
            await _context.SaveChangesAsync();

            return holiday;
        }


    }
}
