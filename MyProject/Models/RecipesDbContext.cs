using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace MyProject.Models
{
    public class RecipesDbContext : DbContext
    {
        public RecipesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Instruction> Instruction { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<FoodImage> foodImages { get; set; }
        public DbSet<JewishHoliday> jewishHoliday { get; set; }
        public DbSet<RecipeInHoliday> recipeInHoliday { get; set; }   
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredient>().HasKey(e => new { e.Index, e.RecipeId });
            modelBuilder.Entity<Instruction>().HasKey(e => new { e.Position, e.RecipeId });
            modelBuilder.Entity<Note>().HasKey(e => new { e.NoteNumber, e.RecipeId });
            modelBuilder.Entity<FoodImage>().HasKey(e => new {e.ImageId, e.RecipeId});
            modelBuilder.Entity<JewishHoliday>().HasKey(e => new { e.HolidayId });
            modelBuilder.Entity<RecipeInHoliday>().HasKey(e => new { e.RecipeId, e.HolidayId });
        }

        

    }
}
