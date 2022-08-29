using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        void AddAnimal(Animal animal);
        Animal EditAnimal(Animal animal);
        bool Login(User user);
        void RemoveAnimal(int id);
    }
}