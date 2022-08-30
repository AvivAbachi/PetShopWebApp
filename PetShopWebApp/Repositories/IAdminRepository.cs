using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        void AddAnimal(Animal animal);
        void EditAnimal(Animal animal);
        bool Login(User user);
        void RemoveAnimal(int id);
    }
}