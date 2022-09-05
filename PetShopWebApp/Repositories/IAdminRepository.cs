using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Task AddPet(Pet pet);
        Task<bool> EditPet(Pet pet);
        Task<string> UploadPicture(IFormFile file, int id);
        bool RemovePet(int id);
    }
}