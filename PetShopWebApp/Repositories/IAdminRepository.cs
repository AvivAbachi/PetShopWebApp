using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Pet AddPet(Pet pet);
        bool EditPet(Pet pet);
        Task UploadPicture(Pet pet);
        bool RemovePet(int id);
    }
}