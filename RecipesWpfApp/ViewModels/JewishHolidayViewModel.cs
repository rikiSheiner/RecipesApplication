using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.JewishHolidayCommands;
using RecipesWpfApp.Commands.RecipeCommands;
using RecipesWpfApp.Models;
using RecipesWpfApp.Services;
using RecipesWpfApp.Stores;

namespace RecipesWpfApp.ViewModels
{
    internal class JewishHolidayViewModel: ViewModelBase
    {
        private JewishHoliday _jewishHolidayToAdd;
        public JewishHoliday JewishHolidayToAdd
        {
            get { return _jewishHolidayToAdd; }
            set
            {
                if (_jewishHolidayToAdd != value)
                {
                    _jewishHolidayToAdd = value;
                    OnPropertyChanged(nameof(JewishHolidayToAdd));
                }
            }
        }

        private ObservableCollection<JewishHoliday> _holidays;
        public ObservableCollection<JewishHoliday> Holidays
        {
            get { return _holidays; }
            set
            {
                if(_holidays != value)
                {
                    _holidays = value;
                    OnPropertyChanged(nameof(Holidays));    
                }
            }
        }

        private ObservableCollection<JewishHoliday> _allHolidays;
        public ObservableCollection<JewishHoliday> AllHolidays
        {
            get { return _allHolidays; }
            set
            {
                if (_allHolidays != value)
                {
                    _allHolidays = value;
                    OnPropertyChanged(nameof(AllHolidays));
                }
            }
        }

        private JewishHoliday _selectedJewishHoliday;
        public JewishHoliday SelectedJewishHoliday
        {
            get { return _selectedJewishHoliday; }
            set
            {
                if(_selectedJewishHoliday != value)
                {
                    _selectedJewishHoliday = value;
                    OnPropertyChanged(nameof(SelectedJewishHoliday));
                }
            }
        }

        private bool _isInAddingHoliday;
        public bool IsInAddingHoliday
        {
            get { return _isInAddingHoliday; }
            set
            {
                if(_isInAddingHoliday != value)
                {
                    _isInAddingHoliday = value;
                    OnPropertyChanged(nameof(IsInAddingHoliday));
                }
            }
        }

        // The details of the selected recipe
        private RecipeDetails _recipeDetails;
        public RecipeDetails RecipeDetails
        {
            get { return _recipeDetails; }
            set
            {
                if (_recipeDetails != value)
                {
                    _recipeDetails = value;
                    OnPropertyChanged(nameof(RecipeDetails));
                }
            }
        }

        public string ApiHolidayUrl { get; }


        private SingleRecipeViewModel _singleRecipeViewModel;
        private NavigationStore _navigationStore;

        public ICommand LoadHolidaysOfRecipeCommand { get; }
        public ICommand LoadAllHolidaysCommand { get; }
        public ICommand AddHolidayStateCommand { get; }
        public ICommand AddJewishHolidayCommand { get; }
        public ICommand GetJewishHolidayDetailsCommand { get; }

        public JewishHolidayViewModel(SingleRecipeViewModel singleRecipeViewModel, NavigationStore navigationStore)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            _navigationStore = navigationStore;

            ApiHolidayUrl = "https://localhost:7079/api/JewishHoliday";
            
            IsInAddingHoliday = false;

            RecipeDetails = _singleRecipeViewModel.RecipeDetails;

            JewishHolidayToAdd = new JewishHoliday();

            LoadHolidaysOfRecipeCommand = new LoadHolidaysOfRecipeCommands(this);
            LoadAllHolidaysCommand = new LoadAllHolidaysCommand(this);
            AddHolidayStateCommand = new AddHolidayStateCommand(this);
            AddJewishHolidayCommand = new AddJewishHolidayCommand(this);

            ParameterNavigationService<JewishHoliday, JewishHolidayDetailsViewModel> navigationService =
                    new ParameterNavigationService<JewishHoliday, JewishHolidayDetailsViewModel>(navigationStore,
                    (parameter) => new JewishHolidayDetailsViewModel(parameter, navigationStore));

            GetJewishHolidayDetailsCommand = new SelectJewishHolidayCommand(this, navigationService);
        }
    }
}
