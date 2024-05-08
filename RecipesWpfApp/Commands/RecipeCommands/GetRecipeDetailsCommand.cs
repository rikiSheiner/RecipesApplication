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
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של קבלת פרטי מתכון מסוים
    /// </summary>
    internal class GetRecipeDetailsCommand : AsyncCommandBase
    {
        public NavigationBarViewModel NavigationBarViewModel { get; }
        private SearchRecipeViewModel _searchRecipeViewModel;
        private readonly NavigationStore _navigationStore;

        private ICommand SelectRecipeCommand { get; }

        private const string BASE_URL_REST_API = "https://tasty.p.rapidapi.com/";
        private const string MORE_INFO_ENDPOINT = "recipes/get-more-info?id={0}";
        
        /*בזה השתמשתי עד שניצלתי עד תום את הגרסה החינמית
        * API KEY of r0587860843@gmail.com account
        * private const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
        */

        // ולכן החלפתי חשבון
        // API KEY of rsheiner@g.jct.ac.il account
        private const string API_KEY = "c056d8e245msh1dbf79f218783a1p17409bjsna5612c62ffa4";

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
        
        // הפקודה המתבצעת בע הרצת הפקודה לקבלת פרטי מתכון מסוים
        public override async Task ExecuteAsync(object parameter)
        {

            RecipeDetails recipeDetails;
            int recipeId = _searchRecipeViewModel.SelectedRecipe.Id;

            // שליחת בקשת HTTP לAPI על מנת לקבל את הפרטים של המתכון הנבחר
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
                // משתמשים בתגובה שהתקבלה המכילה את כל פרטי המתכון
                // ויוצרים ממנה אוביקט של מתכון המכיל את כל הפרטים

                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                recipeDetails = JsonConvert.DeserializeObject<RecipeDetails>(responseContent);

                dynamic jsonObject = JsonConvert.DeserializeObject(responseContent);

                string rawText;
                int position = 0;
                string display_text;

                // מוסיפים את כל הרכיבים של המתכון
                foreach (var item in jsonObject["sections"][0]["components"])
                {
                    rawText = item["raw_text"].ToString();
                    if (rawText != "n/a")
                    {
                        position++;
                        var ingredient = new Ingredient(position, recipeId, rawText);
                        recipeDetails.Ingredients.Add(ingredient);
                    }
                }

                position = 0;
                recipeDetails.Instructions = new List<Instruction>();
                // מוסיפים את כל ההוראות של המתכון
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

            // מנווטים אל עמוד של מתכון יחיד המכיל את פרטי המתכון הנבחר
            ParameterNavigationService<RecipeDetails, SingleRecipeViewModel> navigationService =
                new ParameterNavigationService<RecipeDetails, SingleRecipeViewModel>(_navigationStore,
                (p) => new SingleRecipeViewModel( NavigationBarViewModel, p, false, _navigationStore));

            new SelectRecipeCommand(_searchRecipeViewModel, navigationService).Execute("false");
        }
    }
}
