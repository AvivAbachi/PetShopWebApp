using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;
using System.Net;

namespace PetShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicRepository _repository;

        public HomeController(IPublicRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.GetPetsByLikes(2));
        }

        [Route("Pet/{id}")]
        public IActionResult Pet(int id)
        {
            var pet = _repository.GetPetByIDAndComments(id);
            if (pet == null) return RedirectToAction("Error");
            return View(pet);
        }

        [Route("Category")]
        public IActionResult Category(int? id)
        {
            ViewBag.CategoryList = _repository.GetCategories();
            return View(id == null ?
                _repository.GetPets() :
                _repository.GetPetByCategory(id!.Value));
        }

        [HttpPost]
        public IActionResult AddPetLike(int id)
        {
            var pet = _repository.AddPetLike(id);
            if (pet == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Pet Not Found!" });
            }
            return Json(pet.Like);
        }

        [HttpPost]
        public IActionResult AddPetComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedDate = DateTime.Now;
                if (_repository.AddAnimaComment(comment))
                {
                    var jsonComment = new
                    {
                        comment!.Auther,
                        comment!.Text,
                        CreatedDate = comment!.CreatedDate.ToString()
                    };
                    return Json(jsonComment);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Invalid comment" });
        }

        [Route("{*url}", Order = 999)]
        public IActionResult Error()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}
