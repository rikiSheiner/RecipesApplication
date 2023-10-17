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
    internal class UpdateRecipeDetailsCommand : AsyncCommandBase
    {
        private SingleRecipeViewModel _singleRecipeViewModel;
        public UpdateRecipeDetailsCommand(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();
            string apiUrl = "https://localhost:7079/api/Recipe";

            var recipeDetails = _singleRecipeViewModel.RecipeDetails;
            int id = recipeDetails.Id;

            string serializedRecipe = JsonConvert.SerializeObject(recipeDetails);

            HttpResponseMessage saveResponse = await client.PutAsync(apiUrl + $"/{id}", new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

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
