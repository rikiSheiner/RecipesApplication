using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class InstructionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
