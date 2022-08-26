using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Repositories;

namespace PetShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly IPublicRepository _repository;
        public HomeController(IPublicRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.GetAnimalsByLikes(2));
        }
        public IActionResult Animal(int id)
        {
            return View(_repository.GetAnimalByIDAndComments(id));
        }
        [HttpPost]
        public JsonResult AddAnimalLike(string id)
        {
            _ = int.TryParse(id, out int num);
            return Json(_repository.AddAnimalLike(num));
            //return RedirectToAction("Animal", new { id = num });
            //return NoContent();
        }
    }
}
