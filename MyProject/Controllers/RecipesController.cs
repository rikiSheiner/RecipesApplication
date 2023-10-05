using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[SwaggerTag("RecipeController")]
    public class RecipeController : ControllerBase
    {

        private readonly RecipesDbContext _context;

        public RecipeController(RecipesDbContext context)
        {
            _context = context;
        }

        #region Recipe Actions

        // GET: api/Recipe
        [HttpGet]
        //[Route("get/all")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.Include("Ingredients")
                .Include("Instructions").Include("Notes").ToListAsync();
        }


        // PUT: api/Recipe/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            _context.Recipes.Update(recipe);

            foreach (var item in recipe.Ingredients)
            {
                var found = await _context.Ingredient.FirstOrDefaultAsync
                    (x => x.Index == item.Index && x.RecipeId == item.RecipeId) != null;
                if (found)
                    _context.Ingredient.Update(item);
                else
                    _context.Ingredient.Add(item);
            }

            foreach (var item in recipe.Instructions)
            {
                var found = await _context.Instruction.FirstOrDefaultAsync
                    (x => x.Position == item.Position && x.RecipeId == item.RecipeId) != null;
                if (found)
                    _context.Instruction.Update(item);
                else
                    _context.Instruction.Add(item);
            }

            foreach (var item in recipe.Notes)
            {
                var found = await _context.Note.FirstOrDefaultAsync
                    (x => x.NoteNumber == item.NoteNumber && x.RecipeId == item.RecipeId) != null;
                if (found)
                    _context.Note.Update(item);
                else
                    _context.Note.Add(item);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        // GET: api/Recipe/5
        [HttpGet("{id}")]
        //[Route("get/one")]

        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.Include("Ingredients")
                .Include("Instructions").Include("Notes").FirstAsync(e => e.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }


        // POST: api/Recipe
        [HttpPost]
        //[Route("post")]

        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        //[Route("delete")]
        public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.Include("Ingredients")
                .Include("Instructions").Include("Notes").FirstAsync(e => e.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            _context.Ingredient.RemoveRange(recipe.Ingredients);
            _context.Instruction.RemoveRange(recipe.Instructions);
            _context.Note.RemoveRange(recipe.Notes);

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }


        #endregion


        #region Images
        // POST: api/Recipe
        [HttpPost]
        [Route("image")]

        public async Task<ActionResult<FoodImage>> PostFoodImage([FromForm] FoodImage foodImage)
        {
            if (ModelState.IsValid)
            {
                // save image 
                string fileName = Path.GetFileNameWithoutExtension(foodImage.ImageFile.FileName);
                string extension = Path.GetExtension(foodImage.ImageFile.FileName);
                foodImage.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine("C://Users//1//Source//Repos//MyFinalProject2023//MyProject//Images//", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await foodImage.ImageFile.CopyToAsync(fileStream);
                }

                // save the record
                _context.foodImages.Add(foodImage);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("PostFoodImage", new { id = foodImage.ImageId }, foodImage);
        }


        [HttpGet]
        [Route("image")]

        public async Task<ActionResult<FoodImage>> GetFoodImage(int recipeId)
        {
            var foodImage = await _context.foodImages.FirstAsync(e => e.RecipeId == recipeId);

            if (foodImage == null)
            {
                return NotFound();
            }

            return foodImage;
        }

        /*
        [HttpDelete("{recipeId}")]
        [Route("image")]
        public async Task<ActionResult<FoodImage?>> DeleteConfirmed(int recipeId) 
        {
            var foodImage = _context.foodImages.FirstOrDefaultAsync(x => x.RecipeId == recipeId).Result;

            // delete the image from the file
            if(foodImage != null)
            {
                var imagePath = Path.Combine("Images/", foodImage.ImageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

                // delete the record
                _context.foodImages.Remove(foodImage);
                await _context.SaveChangesAsync();
            };

            return foodImage;
        }*/

        #endregion



        #region Holidays

        [HttpGet]
        [Route("holiday")]
        public async Task<ActionResult<IEnumerable<JewishHoliday>>> GetHolidays()
        {
            return await _context.jewishHoliday.ToListAsync();
        }


        [HttpPut()]
        [Route("holiday")]
        public async Task<IActionResult> PutHoliday(int id, JewishHoliday jewishHoliday)
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


        [HttpGet()]
        [Route("holiday/one")]
        public async Task<ActionResult<JewishHoliday>> GetHoliday(string name)
        {
            var holiday = await _context.jewishHoliday.FirstAsync(e => e.Name == name);

            if (holiday == null)
            {
                return NotFound();
            }

            return holiday;
        }

        //[HttpGet()]
        //[Route("holiday/one")]
        //public async Task<ActionResult<JewishHoliday>> GetHoliday(int id)
        //{
        //    var holiday = await _context.jewishHoliday.FirstAsync(e => e.HolidayId == id);

        //    if (holiday == null)
        //    {
        //        return NotFound();
        //    }

        //    return holiday;
        //}


        [HttpPost]
        [Route("holiday")]
        public async Task<ActionResult<JewishHoliday>> PostHoliday(JewishHoliday holiday)
        {
            _context.jewishHoliday.Add(holiday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoliday", new { id = holiday.HolidayId }, holiday);
        }

        [HttpDelete()]
        [Route("holiday")]
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


        #endregion



        // לעניות דעתי כדאי להפריד בין פעולות של מתכון לבין פעולות של מתכון בחג
        // על מנת שלא נידרש לעדכן את המתכון בכל פעם שנרצה להוסיף לו מועד שבו הוכן
        // כי במתכון יש המון שדות וכן שדות ששייכים לטבלאות אחרות
        // ולכן פעולת עדכון המתכון אורכת זמן רב

        #region RecipeInHoliday

        [HttpGet]
        [Route("recipe-in-holiday")]
        public async Task<ActionResult<IEnumerable<RecipeInHoliday>>> GetRecipesInHolidays()
        {
            return await _context.recipeInHoliday.ToListAsync();
        }


        [HttpPut()]
        [Route("recipe-in-holiday")]
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
        [Route("recipe-in-holiday/one")]
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
        [Route("recipe-in-holiday")]
        public async Task<ActionResult<RecipeInHoliday>> PostRecipeInHoliday(RecipeInHoliday recipeInHoliday)
        {
            _context.recipeInHoliday.Add(recipeInHoliday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeInHoliday", new { id = recipeInHoliday.HolidayId }, recipeInHoliday);
        }

        [HttpDelete()]
        [Route("recipe-in-holiday")]
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


        #endregion
    }
}



