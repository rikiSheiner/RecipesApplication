using RecipesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.ViewModels
{
    internal class InstructionsViewModel : ViewModelBase
    {
        private List<Instruction> _instructions;
        public List<Instruction> Instructions
        {
            get { return _instructions; }
            set
            {
                if (_instructions != value)
                {
                    _instructions = value;
                    OnPropertyChanged(nameof(Instructions));
                }
            }
        }

        public InstructionsViewModel()
        {
                
        }
    }
}
