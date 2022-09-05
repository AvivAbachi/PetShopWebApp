using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Animal AddAnimal(Animal pet);
        Animal? EditAnimal(Animal editPet);
        Task UploadPicture(Animal pet);
        bool RemoveAnimal(int id);
    }
}