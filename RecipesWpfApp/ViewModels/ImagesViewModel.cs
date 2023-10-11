using Microsoft.AspNetCore.Http;
using RecipesWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipesWpfApp.ViewModels
{
    internal class ImagesViewModel : ViewModelBase
    {
        private List<IFormFile> _images;
        public List<IFormFile> Images
        {
            get { return _images; }
            set
            {
                if(_images != value)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        public ICommand AddImageCommand;
        public ICommand RemoveImageCommand;

        
    }
}
