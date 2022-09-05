using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
	public interface IPublicRepository
	{
		IEnumerable<Pet> GetPets();
		IEnumerable<Pet> GetPetByCategory(int category);
		IEnumerable<Pet> GetPetsByLikes(int counter);
		Pet? GetPetByIDAndComments(int id);
		int AddPetLike(int id);
		Comment AddPetComment(int id, string auther, string comment);
		IEnumerable<Comment> GetComments();
		IEnumerable<Category> GetCategories();
	}
}
