using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipesWpfApp.Commands.RecipeCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של שמירת מתכון חדש!! בבסיס הנתונים
    /// </summary>
    internal class SaveRecipeCommand : AsyncCommandBase
    {
        SingleRecipeViewModel _singleRecipeViewModel;
        public SaveRecipeCommand(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
        }
        
        // הפעולה המתבצעת בעת הרצת פקודה של שמירת מתכון
        public override async Task ExecuteAsync(object parameter)
        {
            // שולחים בקשת HTTP לAPI של המתכונים 
            // כדי לשמור את פרטי המתכון בבסיס הנתונים
            HttpClient client = new HttpClient();
            string apiUrl = "https://localhost:7079/api/Recipe";

            string serializedRecipe = JsonConvert.SerializeObject(_singleRecipeViewModel.RecipeDetails);

            HttpResponseMessage saveResponse = await client.PostAsync(apiUrl, new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

            if (saveResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Recipe saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _singleRecipeViewModel.IsNotSaved = false;
            }
            else
            {
                MessageBox.Show("Failed to save the recipe. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
