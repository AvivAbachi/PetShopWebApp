using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;

namespace PetShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        IAdminRepository _repository;
        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {

            bool? isUser = (bool?)TempData["user"];
            bool islogin = isUser.HasValue && isUser.Value;
            return islogin ? View() : View("./Login", new User());
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            bool islogin = _repository.Login(user);
            TempData["user"] = islogin;
            return RedirectToAction("Index");
        }
    }
}
