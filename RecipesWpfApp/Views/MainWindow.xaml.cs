using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //בחלון זה נציג מסך פתיחה של האפליקציה שלנו
        // תיהיה כותרת והסבר קצת ולינקים לחלונות שמכילים מידע
        // תיהיה הפניה לחלון של חיפוש מתכונים
        // תיהיה גם הפניה לחלון של ספר מתכונים

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            new SearchRecipeWindow().Show();
            //this.Close();

        }

        private void ViewRecipes_Click(object sender, RoutedEventArgs e)
        {
            //new LoadAllRecipes();
            new RecipesBookWindow().Show();
            //this.Close();

        }
    }
}
