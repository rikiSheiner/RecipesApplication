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

namespace RecipesWpfApp.Views
{
    internal class LoadSingleRecipe
    {
        private const string BASE_URL_REST_API = "https://tasty.p.rapidapi.com/";
        private const string MORE_INFO_ENDPOINT = "recipes/get-more-info?id={0}";
        private const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
        private const string API_HOST = "tasty.p.rapidapi.com";

        private const string API_BASE_URL = "https://localhost:7079/api";

        private int recipeId;
        private RecipeDetails recipeDetails;

        public LoadSingleRecipe(int id)
        {
            recipeId = id;
            LoadRecipeData();
        }

        private async void LoadRecipeData()
        {
            RecipeDetailsViewModel  recipeViewModel = new RecipeDetailsViewModel();
            recipeViewModel.RecipeDetails = await GetRecipeFromApi(); 
            new SingleRecipeWindow(recipeViewModel.RecipeDetails).Show();
        }

        private async Task<RecipeDetails> GetRecipeFromApi()
        {
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

                recipeDetails.Ingredients = new List<Ingredient>();
                recipeDetails.Instructions = new List<Instruction>();

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
            }
            return recipeDetails;
        }
    }
}
