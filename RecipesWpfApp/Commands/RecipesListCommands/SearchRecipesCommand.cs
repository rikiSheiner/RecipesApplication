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
    internal class SearchRecipesCommand : AsyncCommandBase
    {
        const string BASE_URL = "https://tasty.p.rapidapi.com/";
        const string LIST_ENDPOINT = "recipes/list?from=0&size=20&tags=under_30_minutes&q={0}";
        const string API_KEY = "988b7634ecmsh14462af722fa434p138e04jsn2cb3a07a28a8";
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

        public override async  Task ExecuteAsync(object parameter)
        {
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
