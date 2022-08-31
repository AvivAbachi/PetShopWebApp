using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Task AddAnimal(Animal animal);
        Task EditAnimal(Animal animal);
        bool Login(User user);
        void RemoveAnimal(int id);
    }
}