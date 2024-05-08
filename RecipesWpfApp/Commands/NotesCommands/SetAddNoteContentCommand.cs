using RecipesWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands.NotesCommands
{
    /// <summary>
    /// מחלקה המשמשת לייצוג פקודה של עדכון תוכן הערה להוספה
    /// ברגע שלוחצים על תיבת הטקסט של הוספת הערה מנקים את התיבה
    /// מתוכן בררת המחדל שלה על מנת לאפשר הקלדה של ההערה להוספה
    /// </summary>
    internal class SetAddNoteContentCommand : CommandBase
    {
        private NotesViewModel _notesViewModel;

        public SetAddNoteContentCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }
        
        // פעולה המתבצעת בעת הרצת הפקודה לניקוי תיבת הטקסט של הערה להוספה
        public override void Execute(object parameter)
        {
            _notesViewModel.NoteToAdd = "";
            _notesViewModel.IsInAdding = true;
        }
    }
}
