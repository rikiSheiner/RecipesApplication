using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.Services;
using RecipesWpfApp.Stores;
using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipesWpfApp.Commands.RecipeCommands
{
    internal class GetRecipeDetailsCommand : AsyncCommandBase
    {
        public NavigationBarViewModel NavigationBarViewModel { get; }
        private SearchRecipeViewModel _searchRecipeViewModel;
        private readonly NavigationStore _navigationStore;

        private ICommand SelectRecipeCommand { get; }

        private const string BASE_URL_REST_API = "https://tasty.p.rapidapi.com/";
        private const string MORE_INFO_ENDPOINT = "recipes/get-more-info?id={0}";
        private const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
        private const string API_HOST = "tasty.p.rapidapi.com";
        private const string API_BASE_URL = "https://localhost:7079/api";

        public GetRecipeDetailsCommand(SearchRecipeViewModel searchRecipeViewModel, NavigationBarViewModel navigationBarViewModel,
            NavigationStore navigationStore)
        {
            NavigationBarViewModel = navigationBarViewModel;
            _searchRecipeViewModel = searchRecipeViewModel;
            _navigationStore = navigationStore;


            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService =
                    new ParameterNavigationService<RecipeDetails, SingleRecipeViewModel>(_navigationStore,
                    (parameter) => new SingleRecipeViewModel(navigationBarViewModel,parameter, false, _navigationStore));

            new SelectRecipeCommand(_searchRecipeViewModel, navigationService);
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
                recipeDetails.Instructions = new List<Instruction>();
                foreach (var item in jsonObject["instructions"])
                {
                    display_text = item["display_text"].ToString();
                    if(display_text != null && display_text.Length > 0)
                    {
                        position++;
                        var instruction = new Instruction(recipeId, position, display_text);
                        recipeDetails.Instructions.Add(instruction);
                    }
                    
                }
                recipeDetails.Notes = new List<Note>();
            }

            _searchRecipeViewModel.SelectedRecipeDetails = recipeDetails;


            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService =
                new ParameterNavigationService<RecipeDetails, SingleRecipeViewModel>(_navigationStore,
                (p) => new SingleRecipeViewModel( NavigationBarViewModel, p, false, _navigationStore));

            new SelectRecipeCommand(_searchRecipeViewModel, navigationService).Execute("false");
        }
    }
}
