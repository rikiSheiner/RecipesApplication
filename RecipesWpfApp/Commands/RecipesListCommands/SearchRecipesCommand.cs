using Newtonsoft.Json;
using RecipesWpfApp.Models;
using RecipesWpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using RecipesWpfApp.ViewModels;
using System.Collections;

namespace RecipesWpfApp.Commands.RecipesListCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של חיפוש מתכון בREST API
    /// החיפוש מתבצע על פי רכיבי מתכון או שם המתכון
    /// </summary>
    internal class SearchRecipesCommand : AsyncCommandBase
    {
        const string BASE_URL = "https://tasty.p.rapidapi.com/";
        const string LIST_ENDPOINT = "recipes/list?from=0&size=20&tags=under_30_minutes&q={0}";

        // במפתח זה השתמשתי עד שניצלתי אותו עד תום
        // API KEY of r0587860843@gmail.com account
        //private const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
        
        // ואז יצרתי מפתח חדש 
        // API KEY of rsheiner@g.jct.ac.il account
        private const string API_KEY = "c056d8e245msh1dbf79f218783a1p17409bjsna5612c62ffa4";

        const string API_HOST = "tasty.p.rapidapi.com";

        private readonly HttpClient _httpClient;
        private readonly SearchRecipeViewModel _searchRecipeViewModel;

        public SearchRecipesCommand(SearchRecipeViewModel searchRecipeViewModel)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL)
            };
            _searchRecipeViewModel = searchRecipeViewModel;

        }

        // הפעולה שמתבצעת בעת הרצת פקודה לחיפוש מתכון
        public override async Task ExecuteAsync(object parameter)
        {
            // שולחים בקשת GET לREST API 
            // על פי מילות החיפוש שהוזנו בתיבת החיפוש
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BASE_URL + string.Format(LIST_ENDPOINT, _searchRecipeViewModel.Query)),
                Headers =
                {
                    { "X-RapidAPI-Key", API_KEY },
                    { "X-RapidAPI-Host", API_HOST },
                },
            };

            try
            {
                _searchRecipeViewModel.IsLoading = true;
                // מקבלים את התגובה וממירים אותה לרשימת מתכונים
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    string responseContent = await response.Content.ReadAsStringAsync();

                    RecipeList items = JsonConvert.DeserializeObject<RecipeList>(responseContent);

                    _searchRecipeViewModel.Recipes = items.results;
                }
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
            finally
            {
                _searchRecipeViewModel.IsLoading = false;

            }
        }
    }
}
