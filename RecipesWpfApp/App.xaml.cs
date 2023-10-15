using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RecipesWpfApp.Windows;
using RecipesWpfApp.ViewModels;
using RecipesWpfApp.Stores;
using RecipesWpfApp.Services;

namespace RecipesWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly NavigationBarViewModel _navigationBarViewModel;

        public App()
        {
            _navigationStore = new NavigationStore();
            _navigationBarViewModel = new NavigationBarViewModel(
                CreateHomeNavigationService(),
                CreateSearchRecipesNavigationService(),
                CreateRecipesBookNavigationService());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationService<HomeViewModel> homeNavigationService = CreateHomeNavigationService();
            homeNavigationService.Navigate();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private NavigationService<RecipesBookViewModel> CreateRecipesBookNavigationService()
        {
            return new NavigationService<RecipesBookViewModel>(
                _navigationStore,
                () => new RecipesBookViewModel(_navigationBarViewModel, _navigationStore));
        }

        private NavigationService<SearchRecipeViewModel> CreateSearchRecipesNavigationService()
        {
            return new NavigationService<SearchRecipeViewModel>(
                _navigationStore,
                () => new SearchRecipeViewModel(_navigationBarViewModel, _navigationStore));
        }

        private NavigationService<HomeViewModel> CreateHomeNavigationService()
        {
            return new NavigationService<HomeViewModel>(
                _navigationStore,
                () => new HomeViewModel(_navigationBarViewModel,
                CreateSearchRecipesNavigationService(), CreateRecipesBookNavigationService()));
        }

        
    }
}
