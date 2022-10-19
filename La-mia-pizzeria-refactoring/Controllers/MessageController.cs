using Microsoft.AspNetCore.Mvc;

namespace La_mia_pizzeria_refactoring.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
