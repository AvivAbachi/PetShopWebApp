using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public interface IPublicRepository
    {
        IEnumerable<Animal> GetAnimals();
        IEnumerable<Animal> GetAnimalByCategory(int category);
        IEnumerable<Animal> GetAnimalsByLikes(int counter);
        Animal? GetAnimalByIDAndComments(int id);
        int  AddAnimalLike(int id);
        Comment AddAnimaComment(int id, string auther, string comment);
        IEnumerable<Comment> GetComments();
        IEnumerable<Category> GetCategories();
    }
}
