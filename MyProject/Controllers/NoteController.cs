using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
