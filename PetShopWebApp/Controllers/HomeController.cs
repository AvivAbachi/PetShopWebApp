using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;
using System;
using System.Text.Json;

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

        public IActionResult Category(int? id)
        {
            
            ViewBag.CategoryList = _repository.GetCategories();
            return View(id == null ? _repository.GetAnimals() :_repository.GetAnimalByCategory(id!.Value)) ;
        }

        [HttpPost]
        public JsonResult AddAnimalLike(int id)
        {
            return Json(_repository.AddAnimalLike(id));
            //return RedirectToAction("Animal", new { id });
            //return NoContent();
        }

        [HttpPost]
        public JsonResult AddAnimalComment(int id, string auther, string text)
        {
            var comment = _repository.AddAnimaComment(id, auther, text);
            return Json(new
            {
                comment.CommentId,
                comment.AnimalId,
                comment.Auther,
                comment.Text,
                CreatedDate = comment.CreatedDate.ToString(),
            });
            //_repository.AddAnimaComment(id, auther, text);
            //return RedirectToAction("Animal", new { id });
        }
    }
}
