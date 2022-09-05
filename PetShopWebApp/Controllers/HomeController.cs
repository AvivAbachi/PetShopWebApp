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
			return View(_repository.GetPetsByLikes(2));
		}

		[Route("Pet/{id}")]
		public IActionResult Pet(int id)
		{
			return View(_repository.GetPetByIDAndComments(id));
		}

		[Route("Category")]
		public IActionResult Category(int? id)
		{

			ViewBag.CategoryList = _repository.GetCategories();
			return View(id == null ? _repository.GetPets() : _repository.GetPetByCategory(id!.Value));
		}

		[HttpPost]
		public IActionResult AddPetLike(int id)
		{
			return Json(_repository.AddPetLike(id));
		}

		[HttpPost]
		public IActionResult AddPetComment(int id, string auther, string text)
		{
			var comment = _repository.AddPetComment(id, auther, text);
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
