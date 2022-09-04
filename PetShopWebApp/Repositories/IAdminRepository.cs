using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
	public interface IAdminRepository
	{
		Task AddAnimal(Animal animal);
		Task<bool> EditAnimal(Animal animal);
		bool RemoveAnimal(int id);
	}
}