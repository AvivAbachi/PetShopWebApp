using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;

namespace PetShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        IAdminRepository _adminRepository;
        IPublicRepository _publicRepository;
        public AdminController(IAdminRepository adminRepository, IPublicRepository publicRepository)
        {
            _adminRepository = adminRepository;
            _publicRepository = publicRepository;

        }

        public IActionResult Index(int? id)
        {
            //bool? isUser = (bool?)TempData["user"];
            //bool islogin = isUser.HasValue && isUser.Value;
            //if (islogin)
            //{
            ViewBag.CategoryList = _publicRepository.GetCategories();
            return View(id == null ?
                _publicRepository.GetAnimals() :
                _publicRepository.GetAnimalByCategory(id!.Value));
            //}
            //return View("./Login", new User());
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            bool islogin = _adminRepository.Login(user);
            TempData["user"] = islogin;
            return RedirectToAction("Index");
        }

        public IActionResult AddAnimal()
        {
            ViewBag.CategoryList = _publicRepository.GetCategories();
            // bool islogin = _adminRepository.Login(user);
            //TempData["user"] = islogin;
            ViewBag.isEdit = false;
            return View("AddEditAnimal", new Animal());
        }
        public IActionResult EditAnimal(int id)
        {
            ViewBag.CategoryList = _publicRepository.GetCategories();
            var animal= _publicRepository.GetAnimalByIDAndComments(id);
            // bool islogin = _adminRepository.Login(user);
            //TempData["user"] = islogin;
            ViewBag.isEdit = true;
            return View("AddEditAnimal", animal);
        }
        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            _adminRepository.AddAnimal(animal);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditAnimal(Animal animal)
        {
            _adminRepository.EditAnimal(animal);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveAnimal(int id)
        {
            _adminRepository.RemoveAnimal(id);
            return RedirectToAction("Index");
        }
    }
}
