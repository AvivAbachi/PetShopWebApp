using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;

namespace PetShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IPublicRepository _publicRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminController(IAdminRepository adminRepository, IPublicRepository publicRepository, SignInManager<IdentityUser> signInManager)
        {
            _adminRepository = adminRepository;
            _publicRepository = publicRepository;
            _signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index(int? id)
        {
            var user = _signInManager.IsSignedIn(User);
            if (user)
            {
                ViewBag.CategoryList = _publicRepository.GetCategories();
                return View(id == null ?
                    _publicRepository.GetAnimals() :
                    _publicRepository.GetAnimalByCategory(id!.Value));
            }
            return View("./Login", new User());
        }
        public IActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "User account locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View("./Login", user);
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
        [Authorize]
        public IActionResult AddAnimal()
        {
            ViewBag.CategoryList = _publicRepository.GetCategories();
            ViewBag.isEdit = false;
            
            return View("AddEditAnimal", new Animal());
        }
        [Authorize]
        public IActionResult EditAnimal(int id)
        {
            ViewBag.CategoryList = _publicRepository.GetCategories();
            var animal = _publicRepository.GetAnimalByIDAndComments(id);
            ViewBag.isEdit = true;
            return View("AddEditAnimal", animal);
        }
        [HttpPost, Authorize]
        public IActionResult AddAnimal(Animal model)
        {
            if (model == null) return NotFound();
            _adminRepository.AddAnimal(model);
            return RedirectToAction("Index");
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> EditAnimal(Animal model)
        {
            if (model == null) return NotFound();
            await _adminRepository.EditAnimal(model);
            return RedirectToAction("Index");
        }
        [HttpPost, Authorize]
        public IActionResult DeleteAnimal(int id)
        {
            _adminRepository.RemoveAnimal(id);
            return RedirectToAction("Index");
        }
    }
}
