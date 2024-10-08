﻿using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// משמש בשביל לייצג מודל של מתכון מסוים
    /// זהו מודל מדולל המשמש להצגת רשימת המתכונים שנשמרו ע"י המשתמש
    /// </summary>
    public class Recipe : ObservableObject
    {
        private int _id;
        public int Id
        {
            get { return _id; }

            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _description;    
        public string Description
        {
            get { return _description; }

            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }


        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { 
                    if(_imagePath != value)
                    {
                        _imagePath = value;
                        OnPropertyChanged(nameof(ImagePath));
                    } 
                }
        }

        public Recipe()
        {
            ImagePath = "C:\\Users\\1\\Source\\Repos\\MyFinalProject2023\\RecipesWpfApp\\Icons\\meal-icon.png";
        }
    }
}
