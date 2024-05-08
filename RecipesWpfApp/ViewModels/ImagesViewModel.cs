using Microsoft.AspNetCore.Http;
using RecipesWpfApp.Commands;
using RecipesWpfApp.Commands.ImagesCommands;
using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RecipesWpfApp.ViewModels
{
    /// <summary>
    /// מחלקה המשמשת לייצוג הלוגיקה של תמונת מתכון
    /// </summary>
    internal class ImagesViewModel : ViewModelBase
    {
        // רשימת תמונות מתכון עבור מתכון מסוים
        private ObservableCollection<FoodImage> _images;
        public ObservableCollection<FoodImage> Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }


        // תמונת מתכון חדשה שרוצים להוסיף
        private FoodImage _imageToAdd;
        public FoodImage ImageToAdd
        {
            get { return _imageToAdd; }
            set
            {
                if (_imageToAdd != value)
                {
                    _imageToAdd = value;
                    OnPropertyChanged(nameof(ImageToAdd));
                }
            }
        }


        // התמונה הנבחרת מבין תמונות המתכון
        // זוהי התמונה שתוצג בגדול ושאר התמונות יוצגו בקטן
        private FoodImage _selectedImage;
        public FoodImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }

        private BitmapImage _bitmapImageToAdd;
        public BitmapImage BitmapImageToAdd
        {
            get { return _bitmapImageToAdd; }
            set
            {
                if(value != _bitmapImageToAdd)
                {
                    _bitmapImageToAdd = value;
                    OnPropertyChanged(nameof(BitmapImageToAdd));
                }
            }
        }

        // פרטי המתכון שרוצים להוסיף לו תמונה
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

        // האם אנו במצב של הוספת תמונה למתכון או לא
        private bool _isInAdding;
        public bool IsInAdding
        {
            get { return _isInAdding; }
            set
            {
                if (_isInAdding != value)
                {
                    _isInAdding = value;
                    OnPropertyChanged(nameof(IsInAdding));
                }
            }
        }

        // כתובת URL של API של התמונות
        public string ApiImageUrl { get; }

        // VM של המתכון הנוכחי
        private SingleRecipeViewModel _singleRecipeViewModel;

       // פקודת טעינת תמונות של מתכון
        public ICommand LoadImagesOfRecipeCommand { get; }
        // פקודת הוספת תמונה למתכון
        public ICommand AddImageCommand { get; }
        // פקודת הסרת תמונה ממתכון
        public ICommand RemoveImageCommand { get; }

        // פקודת חיפוש תמונה למתכון
        public ICommand BrowseImageCommand { get; }
        // פקודת העלאת תמונה חדשה של מתכון
        public ICommand UploadImageCommand { get; }

        public ImagesViewModel(SingleRecipeViewModel singleRecipeViewModel)
        {
            _singleRecipeViewModel = singleRecipeViewModel;
            RecipeDetails = _singleRecipeViewModel.RecipeDetails;
            ApiImageUrl = "https://localhost:7079/api/FoodImage";

            IsInAdding = false;

            LoadImagesOfRecipeCommand = new LoadImagesOfRecipeCommand(this);
            BrowseImageCommand = new BrowseImageCommand(this);
            UploadImageCommand = new UploadImageCommand(this);

        }
    }
}
