using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
	public interface IPublicRepository
	{
		IEnumerable<Animal> GetAnimals();
		IEnumerable<Animal> GetAnimalByCategory(int category);
		IEnumerable<Animal> GetAnimalsByLikes(int counter);
		Animal? GetAnimalByIDAndComments(int id);
        Animal? AddAnimalLike(int id);
		bool AddAnimaComment(Comment comment);
		IEnumerable<Category> GetCategories();
	}
}
