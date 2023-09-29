using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Ingredient
    {
        //[Key]
        public int Index { get; set; }
        // the id of the recipe to which the ingredient belongs
        [ForeignKey("Recipe")]
        public /*Guid*/int RecipeId { get; set; }
        // the ingredient name and amount
        public string Name { get; set; }

        public Ingredient(int index, /*Guid*/int recipeId, string name)
        {
            Index = index;
            RecipeId = recipeId;
            Name = name;
        }
    }

}
