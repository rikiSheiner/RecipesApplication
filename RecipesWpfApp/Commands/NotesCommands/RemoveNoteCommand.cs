using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NotesCommands
{
    internal class RemoveNoteCommand : AsyncCommandBase
    {
        private NotesViewModel _notesViewModel;
        //private int _recipeId;
        public RemoveNoteCommand(NotesViewModel notesViewModel/*, int recipeId*/)
        {
            _notesViewModel = notesViewModel;
            //_recipeId = recipeId;
        }
        public override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
