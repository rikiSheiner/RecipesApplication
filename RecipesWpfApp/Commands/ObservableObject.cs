using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Commands
{
    /// <summary>
    /// מחלקה המייצגת אוביקט שניתן לתצפת עליו
    /// זה אומר שכאשר מתרחש בו שינוי הוא מדווח לכל מי שצופה עליו
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
