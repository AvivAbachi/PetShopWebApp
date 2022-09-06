﻿using Microsoft.AspNetCore.Authorization;
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
                _publicRepository.GetPets() :
                _publicRepository.GetPetByCategory(id!.Value));
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
        public IActionResult AddPet()
        {
            ViewBag.CategoryList = _publicRepository.GetCategories();
            ViewBag.isEdit = false;
            return View("AddEditPet", new Pet());
        }

        [Authorize]
        public IActionResult EditPet(int id)
        {
            ViewBag.CategoryList = _publicRepository.GetCategories();
            var pet = _publicRepository.GetPetByIDAndComments(id);
            if (pet != null)
            {
                ViewBag.isEdit = true;
                return View("AddEditPet", pet);
            }
            return Redirect("/404");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddPet(Pet pet)
        {
            if (ModelState.IsValid)
            {
                _adminRepository.AddPet(pet);
                await _adminRepository.UploadPicture(pet);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _publicRepository.GetCategories();
            ViewBag.isEdit = false;
            return View("AddEditPet", pet);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> EditPet(Pet pet)
        {
            if (ModelState.IsValid)
            {
                var success = _adminRepository.EditPet(pet);
                if (success && pet.File != null) await _adminRepository.UploadPicture(pet);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _publicRepository.GetCategories();
            ViewBag.isEdit = true;
            return View("AddEditPet", pet);
        }

        [HttpPost, Authorize]
        public IActionResult DeletePet(int id)
        {
            if (_adminRepository.RemovePet(id))
            {
                return RedirectToAction("Index");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Invalid pet id" });
        }
    }
}
