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
            var pets = _context.Animals!
                .Include(pets => pets.Category)
                .OrderByDescending(a => a.Like)
                .Take(2);

            return View(pets);
        }
    }
}
