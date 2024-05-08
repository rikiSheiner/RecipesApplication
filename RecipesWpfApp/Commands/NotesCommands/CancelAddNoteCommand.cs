using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NotesCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה לביטול הוספת הערה למתכון
    /// </summary>
    internal class CancelAddNoteCommand : CommandBase
    {
        private NotesViewModel _notesViewModel;

        public CancelAddNoteCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }
        
        // הפעולה המתבצעת בעת ביטול הוספת מתכון
        public override void Execute(object parameter)
        {
            // נעדכן את תצוגת הוספת הערה לבררת מחדל
            _notesViewModel.NoteToAdd = _notesViewModel.DefaultNoteText;
            _notesViewModel.IsInAdding = false;
        }
    }
}
