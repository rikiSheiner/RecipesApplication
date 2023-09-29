namespace MyProject.Models
{
    public class AddRecipeViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }
        public int? Cook_Time_Minutes { get; set; }
        public int Num_Servings { get; set; }
        public int Ranking { get; set; }
        public List<Note> Notes { get; set; }

    }
}
