using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Task AddAnimal(Animal animal);
        Task<bool> EditAnimal(Animal animal);
        Task<string> UploadPicture(IFormFile file, int id);
        bool RemoveAnimal(int id);
    }
}