using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    /// <summary>
    /// ממחלקה זו משמשת לייצוג מודל של רשימת מתכונים 
    /// הייצוג של מתכון הוא על פי API
    /// </summary>
    public class RecipeModel
    {
        public IEnumerable<ApiModel> results { get; set; }
    }
}
