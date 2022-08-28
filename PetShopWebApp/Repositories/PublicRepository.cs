using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public class PublicRepository : IPublicRepository
    {
        PetShopConetex _context;
        public PublicRepository(PetShopConetex context)
        {
            _context = context;
        }
        public IEnumerable<Animal> GetAnimals()
        {
            return _context.Animals!;
        }
        public Animal GetAnimalByIDAndComments(int id)
        {
            return _context.Animals!
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .First(p => p.AnimalId == id);
        }
        public IEnumerable<Animal> GetAnimalByCategory(int category)
        {
            return _context.Animals!
               .Include(p => p.Category)
               .Where(c => c.CategoryId == category);
        }
        public IEnumerable<Animal> GetAnimalsByLikes(int counter)
        {
            return _context.Animals!
                .OrderByDescending(a => a.Like)
                .Take(counter);
        }
        public void AddAnimalLike(int id)
        {
            _context.Animals!.First(p => p.AnimalId == id).Like++;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Category!;
        }
    }
}
