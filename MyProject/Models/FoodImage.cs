using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProject.Models
{
    public class FoodImage
    {
        [Key]
        public int ImageId { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        public string Title { get; set; }
        [Column(TypeName ="nvrachar(100)")]
        public string ImageName { get; set; }
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        [NotMapped]
        //[DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
