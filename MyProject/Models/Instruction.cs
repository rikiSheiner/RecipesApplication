using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Instruction
    {
        // the id of the recipe to which the instruction belongs
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        // the index of the instrcution in the cooking sequence
        public int Position { get; set; }
        // the instruction content
        public string Text { get; set; }

        public Instruction(int recipeId, int position, string text)
        {
            RecipeId = recipeId;
            Position = position;
            Text = text;
        }
    }

}
