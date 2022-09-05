using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public class PublicRepository : IPublicRepository
    {
        private readonly PetShopConetex _context;

        public PublicRepository(PetShopConetex context)
        {
            _context = context;
        }

        public IEnumerable<Animal> GetAnimals()
        {
            return _context.Animals!
                .OrderBy(a => a.AnimalId);
        }

        public Animal? GetAnimalByIDAndComments(int id)
        {
            return _context.Animals!
                  .Where(p => p.AnimalId == id)
                  .Include(p => p.Category)
                  .Include(p => p.Comments!.OrderByDescending(c => c.CreatedDate))
                  .FirstOrDefault();
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
                .Take(counter)
                .OrderBy(a => a.AnimalId)
                .Include(p => p.Category);
        }

        public Animal? AddAnimalLike(int id)
        {
            var pet = _context.Animals!.FirstOrDefault(p => p.AnimalId == id);
            if (pet != null)
            {
                pet!.Like++;
                _context.SaveChanges();
                return pet;
            }
            return null;
        }

        public bool AddAnimaComment(Comment comment)
        {
            var pet = GetAnimalByIDAndComments(comment.AnimalId);
            if (pet != null)
            {
                comment.CreatedDate = DateTime.Now;
                pet?.Comments!.Add(comment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Category!;
        }
    }
}
