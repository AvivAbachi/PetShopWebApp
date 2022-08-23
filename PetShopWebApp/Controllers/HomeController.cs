using Microsoft.AspNetCore.Mvc;

namespace PetShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
