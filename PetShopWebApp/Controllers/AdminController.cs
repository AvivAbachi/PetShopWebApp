﻿using Microsoft.AspNetCore.Mvc;
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
            ViewBag.CategoryList = _publicRepository.GetCategories();
            bool? isUser = (bool?)TempData["user"];
            bool islogin = isUser.HasValue && isUser.Value;
            if (islogin)
            {
                return View(id == null ?
                    _publicRepository.GetAnimals() :
                    _publicRepository.GetAnimalByCategory(id!.Value));
            }
            return View("./Login", new User());
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            bool islogin = _adminRepository.Login(user);
            TempData["user"] = islogin;
            return RedirectToAction("Index");
        }
    }
}