using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;

namespace PetShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        PetShopConetex _context;
        public HomeController(PetShopConetex conetex)
        {
            _context = conetex;
        }
        public IActionResult Index()
        {
            var pets = _context.Animals!.Where(a => a.AnimalId != 1);
            return View(pets);
        }
    }
}
