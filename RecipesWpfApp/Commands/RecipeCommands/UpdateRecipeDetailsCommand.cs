using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipesWpfApp.Commands.RecipeCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של עדכון פרטי מתכון שמור
    /// </summary>
    internal class UpdateRecipeDetailsCommand : AsyncCommandBase
    {
        private SingleRecipeViewModel _singleRecipeViewModel;
        public UpdateRecipeDetailsCommand(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
        }
        
        // הפעולה המתבצעת בעת הרצת הפקודה של עדכון פרטי מתכון שמור
        public override async Task ExecuteAsync(object parameter)
        {
            // שולחים בקשת HTTP לAPI של המתכונים ושומרים את פרטי המתכון העדכניים
            HttpClient client = new HttpClient();

            var recipeDetails = _singleRecipeViewModel.RecipeDetails;
            int id = recipeDetails.Id;
            
            string apiUrl = $"https://localhost:7079/api/Recipe/{id}";

            string serializedRecipe = JsonConvert.SerializeObject(recipeDetails);

            HttpResponseMessage saveResponse = await client.PutAsync(apiUrl, new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

            if (saveResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }
    }
}
