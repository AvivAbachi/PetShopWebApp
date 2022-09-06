using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IPublicRepository
    {
        IEnumerable<Pet> GetPets();
        IEnumerable<Pet> GetPetByCategory(int category);
        IEnumerable<Pet> GetPetsByLikes(int counter);
        Pet? GetPetByIDAndComments(int id);
        Pet? AddPetLike(int id);
        bool AddAnimaComment(Comment comment);
        IEnumerable<Category> GetCategories();
    }
}
