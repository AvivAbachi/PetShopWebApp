using Microsoft.AspNetCore.Mvc;
using PetShopWebApp.Repositories;

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
			return View(_repository.GetAnimalsByLikes(2));
		}

		[Route("Pet/{id}")]
		public IActionResult Animal(int id)
		{
			return View(_repository.GetAnimalByIDAndComments(id));
		}

		[Route("Category")]
		public IActionResult Category(int? id)
		{

			ViewBag.CategoryList = _repository.GetCategories();
			return View(id == null ? _repository.GetAnimals() : _repository.GetAnimalByCategory(id!.Value));
		}

		[HttpPost]
		public IActionResult AddAnimalLike(int id)
		{
			return Json(_repository.AddAnimalLike(id));
		}

		[HttpPost]
		public IActionResult AddAnimalComment(int id, string auther, string text)
		{
			var comment = _repository.AddAnimaComment(id, auther, text);
			return Json(new
			{
				comment.Auther,
				comment.Text,
				CreatedDate = comment.CreatedDate.ToString(),
			});
		}

		[Route("404")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}
	}
}
