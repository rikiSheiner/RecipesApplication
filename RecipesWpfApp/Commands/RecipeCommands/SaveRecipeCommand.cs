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
    internal class SaveRecipeCommand : AsyncCommandBase
    {
        SingleRecipeViewModel _singleRecipeViewModel;
        public SaveRecipeCommand(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();
            string apiUrl = "https://localhost:7079/api/Recipe";

            string serializedRecipe = JsonConvert.SerializeObject(_singleRecipeViewModel.RecipeDetails);

            HttpResponseMessage saveResponse = await client.PostAsync(apiUrl, new StringContent(serializedRecipe, Encoding.UTF8, "application/json"));

            if (saveResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("success");
                _singleRecipeViewModel.IsNotSaved = false;
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }
    }
}
