using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class FoodImage
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
