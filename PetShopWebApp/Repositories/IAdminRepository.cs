using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
	public interface IAdminRepository
	{
		Task AddPet(Pet pet);
		Task EditPet(Pet pet);
		void RemovePet(int id);
	}
}