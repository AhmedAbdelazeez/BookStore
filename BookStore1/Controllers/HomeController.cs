using Microsoft.AspNetCore.Mvc;

namespace BookStore1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
