using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Pet AddPet(Pet pet);
        bool EditPet(Pet pet);
        Task<Pet?> UploadPicture(int id,IFormFile image);
        bool RemovePet(int id);
    }
}