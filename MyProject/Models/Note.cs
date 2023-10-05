using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Note
    {
        public int NoteNumber { get; set; }
        // the id of the recipe to which the note belongs
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        // the content of the note
        public string Content { get; set; }

        public Note(int noteNumber, int recipeId, string content)
        {
            NoteNumber = noteNumber;
            RecipeId = recipeId;
            Content = content;
        }
    }

}
