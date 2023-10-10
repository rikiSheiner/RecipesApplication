using Newtonsoft.Json;
using RecipesWpfApp.Commands.NavigationCommands;
using RecipesWpfApp.Models;
using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands
{
    internal class GetRecipeDetailsCommand : AsyncCommandBase
    {
        private SearchRecipeViewModel _searchRecipeViewModel;
        private readonly NavigationStore _navigationStore;

        private const string BASE_URL_REST_API = "https://tasty.p.rapidapi.com/";
        private const string MORE_INFO_ENDPOINT = "recipes/get-more-info?id={0}";
        private const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
        private const string API_HOST = "tasty.p.rapidapi.com";
        private const string API_BASE_URL = "https://localhost:7079/api";

        public GetRecipeDetailsCommand(SearchRecipeViewModel searchRecipeViewModel, NavigationStore navigationStore)
        {
            _searchRecipeViewModel = searchRecipeViewModel;
            _navigationStore = navigationStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            RecipeDetails recipeDetails;
            int recipeId = _searchRecipeViewModel.SelectedRecipe.Id;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BASE_URL_REST_API + string.Format(MORE_INFO_ENDPOINT, recipeId)),
                Headers =
                {
                    { "X-RapidAPI-Key", API_KEY },
                    { "X-RapidAPI-Host", API_HOST },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                recipeDetails = JsonConvert.DeserializeObject<RecipeDetails>(responseContent);

                dynamic jsonObject = JsonConvert.DeserializeObject(responseContent);

                string rawText;
                int position = 0;
                string display_text;

                foreach (var item in jsonObject["sections"][0]["components"])
                {
                    position++;
                    rawText = item["raw_text"].ToString();
                    var ingredient = new Ingredient(position, recipeId, rawText);
                    recipeDetails.Ingredients.Add(ingredient);
                }

                position = 0;
                foreach (var item in jsonObject["instructions"])
                {
                    position++;
                    display_text = item["display_text"].ToString();
                    var instruction = new Instruction(recipeId, position, display_text);
                    recipeDetails.Instructions.Add(instruction);
                }
                recipeDetails.Notes = new List<Note>();

                _searchRecipeViewModel.SelectedRecipeDetails = recipeDetails;

                new NavigateToSelectedRecipeCommand(_navigationStore, _searchRecipeViewModel).Execute("false");
            }
        }
    }
}
