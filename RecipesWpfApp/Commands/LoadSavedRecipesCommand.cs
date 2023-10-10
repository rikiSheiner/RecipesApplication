using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipesWpfApp.Commands
{
    internal class LoadSavedRecipesCommand : AsyncCommandBase
    {
        private RecipesBookViewModel _bookViewModel;
        public LoadSavedRecipesCommand(RecipesBookViewModel recipesBookViewModel)
        {
            _bookViewModel = recipesBookViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            HttpClient client = new HttpClient();
            var URL_TO_ASP_API_ENDPOINT = "https://localhost:7079/api/Recipe";


            HttpResponseMessage response = await client.GetAsync(URL_TO_ASP_API_ENDPOINT);

            if (response.IsSuccessStatusCode)
            {
                List<RecipeDetails> recipes;
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    recipes = JsonConvert.DeserializeObject<List<RecipeDetails>>(json);
                    _bookViewModel.AllRecipes = _bookViewModel.Recipes = recipes;
                }
            }
            else
            {
                //MessageBox.Show("ERROR");
            }
        }
    }
}
