using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
	public interface IAdminRepository
	{
		Task AddAnimal(Animal animal);
		Task EditAnimal(Animal animal);
		void RemoveAnimal(int id);
	}
}