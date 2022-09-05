using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        Pet AddPet(Pet pet);
        Pet? EditPet(Pet editPet);
        Task UploadPicture(Pet pet);
        bool RemovePet(int id);
    }
}