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
    }
}
