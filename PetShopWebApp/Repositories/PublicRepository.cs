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
               .Where(p => p.AnimalId == id)
               .Include(p => p.Category)
               .Include(p => p.Comments)
               .First();
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
                .Include(p => p.Category)
                .Take(counter);
        }
        public int AddAnimalLike(int id)
        {
            var pet = _context.Animals!.First(p => p.AnimalId == id);
            pet.Like++;
            _context.SaveChanges();
            return pet.Like;
        }
    }
}
