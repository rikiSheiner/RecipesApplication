using Microsoft.AspNetCore.Http;
using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    public class FoodImage: ObservableObject
    {
        public static int CounterFoodImages = 1;

        private int _imageId;
        public int ImageId
        {
            get { return _imageId; }
            set
            {
                if (_imageId != value)
                {
                    _imageId = value;
                    OnPropertyChanged(nameof(ImageId));
                }
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private string _imageName;
        public string ImageName
        {
            get { return _imageName; }
            set
            {
                if (_imageName != value)
                {
                    _imageName = value;
                    OnPropertyChanged(nameof(ImageName));
                }
            }
        }
        private int _recipeId;
        public int RecipeId
        {
            get { return _recipeId; }
            set
            {
                if (_recipeId != value)
                {
                    _recipeId = value;
                    OnPropertyChanged(nameof(RecipeId));
                }
            }
        }

        
        private IFormFile _imgaeFile;
        public IFormFile ImageFile
        {
            get { return _imgaeFile; }
            set
            {
                if (_imgaeFile != value)
                {
                    _imgaeFile = value;
                    OnPropertyChanged(nameof(ImageFile));
                }
            }
        }

        
        public FoodImage()
        {
            ImageId = CounterFoodImages++;
        }

    }
}
