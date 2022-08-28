using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        void AddAnimal();
        void EditAnimal();
        bool Login(User user);
        void RemoveAnimal();
    }
}