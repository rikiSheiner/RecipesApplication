﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.JewishHolidayCommands;
using RecipesWpfApp.Models;
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

        private string _addingHolidayVisibility;
        public string AddingHolidayVisibility
        {
            get { return _addingHolidayVisibility; }
            set
            {
                if(_addingHolidayVisibility != value)
                {
                    _addingHolidayVisibility = value;
                    OnPropertyChanged(nameof(AddingHolidayVisibility));
                }
            }
        }

        private SingleRecipeViewModel _singleRecipeViewModel;
        private NavigationStore _navigationStore;

        public ICommand AddHolidayStateCommand;
        public ICommand AddJewishHolidayCommand;
        public ICommand GetJewishHolidayDetailsCommand;

        public JewishHolidayViewModel(SingleRecipeViewModel singleRecipeViewModel, NavigationStore navigationStore)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            _navigationStore = navigationStore;

            Holidays = new ObservableCollection<JewishHoliday>();

            AddingHolidayVisibility = "Collapsed";

            AddHolidayStateCommand = new AddHolidayStateCommand(this);
            AddJewishHolidayCommand = new AddJewishHolidayCommand(this);
            GetJewishHolidayDetailsCommand = new NavigateToHolidayDetailsCommand(this, _navigationStore);
        }
    }
}
