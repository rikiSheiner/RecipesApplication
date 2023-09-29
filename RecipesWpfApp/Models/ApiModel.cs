using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWpfApp.Models
{
    public class ApiModel
    {
        public string title { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string sourceUrl { get; set; }
    }
}
