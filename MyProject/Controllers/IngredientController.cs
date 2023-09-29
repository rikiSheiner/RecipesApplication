using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class IngredientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
