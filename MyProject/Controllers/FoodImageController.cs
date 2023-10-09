using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FoodImageController : ControllerBase
    {
        private readonly RecipesDbContext _context;

        public FoodImageController(RecipesDbContext context)
        {
            _context = context;
        }

        // POST: api/Recipe
        [HttpPost]
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


    }
}
