using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;
using System.Net;

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
            ViewBag.CategoryList = _publicRepository.GetCategories();
            return View(id == null ?
                _publicRepository.GetAnimals() :
                _publicRepository.GetAnimalByCategory(id!.Value));
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View(new LoginUserView());
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(LoginUserView user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    //ModelState.AddModelError("login", "Invalid login attempt.");
                    ViewBag.Error = "Invalid login attempt";
                }
            }
            return View("./Login", user);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
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
            var pet = _publicRepository.GetAnimalByIDAndComments(id);
            if (pet != null)
            {
                ViewBag.isEdit = true;
                return View("AddEditAnimal", pet);
            }
            return Redirect("/404");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddAnimal(Animal model)
        {
            if (model.File != null && model.File.ContentType.StartsWith("image/"))
            {
                //ModelState.AddModelError("File", "File type not vaild.");
                //ModelState.SetModelValue("File", "File type not vaild.","");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = _publicRepository.GetCategories();
                ViewBag.isEdit = false;
                return View("AddEditAnimal", model);
            }
            await _adminRepository.AddAnimal(model);
            return RedirectToAction("Index");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> EditAnimal(Animal model)
        {
            if (model.File != null && model.File.ContentType.StartsWith("image/"))
            {
                //ModelState.AddModelError("File", "File type not vaild.");
            }
            if (ModelState.IsValid)
            {
                bool success = await _adminRepository.EditAnimal(model);
                if (success) return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _publicRepository.GetCategories();
            ViewBag.isEdit = true;
            return View("AddEditAnimal", model);
        }

        [HttpPost, Authorize]
        public IActionResult DeleteAnimal(int id)
        {
            if (!_adminRepository.RemoveAnimal(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Invalid Pet Id" });
            }
            return RedirectToAction("Index");
        }
    }
}
