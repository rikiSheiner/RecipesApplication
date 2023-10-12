using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NotesCommands
{
    internal class SetAddNoteContentCommand : CommandBase
    {
        private NotesViewModel _notesViewModel;

        public SetAddNoteContentCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }
        public override void Execute(object parameter)
        {
            _notesViewModel.NoteToAdd = "";
            _notesViewModel.IsInAdding = true;
        }
    }
}
