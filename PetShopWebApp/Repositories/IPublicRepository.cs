using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IPublicRepository
    {
        IEnumerable<Animal> GetAnimals();
        IEnumerable<Animal> GetAnimalByCategory(int category);
        IEnumerable<Animal> GetAnimalsByLikes(int counter);
        Animal GetAnimalByIDAndComments(int id);
        IEnumerable<Category> GetCategories();
        void AddAnimalLike(int id);
    }
}
