using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IRepository
    {
        IEnumerable<Animal> GetAnimals();
        Animal GetAnimalByIDAndComments(int Id);
        Animal GetAnimalByCategory(int category);

    }
}
