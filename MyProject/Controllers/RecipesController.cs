using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        private readonly RecipesDbContext _context;

        public RecipeController(RecipesDbContext context)
        {
            _context = context;
        }

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
        //[Route("update")]
        public async Task<IActionResult> PutRecipe(/*Guid*/int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            _context.Recipes.Update(recipe);

            foreach(var item in recipe.Ingredients)
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

        public async Task<ActionResult<Recipe>> GetRecipe(/*Guid*/int id)
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
        public async Task<ActionResult<Recipe>> DeleteRecipe(/*Guid*/int id)
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

        // POST: api/Recipe
        [HttpPost]
        //[Route("post")]

        public async Task<ActionResult<FoodImage>> PostFoodImage(FoodImage foodImage)
        {
            _context.foodImages.Add(foodImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostFoodImage", new { id = foodImage.ImageId }, foodImage);
        }

        private bool RecipeExists(/*Guid*/int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }




    }
}



