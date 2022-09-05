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

        public IEnumerable<Pet> GetPets()
        {
            return _context.Pets!
                .OrderBy(a => a.PetId);
        }

        public Pet? GetPetByIDAndComments(int id)
        {
            return _context.Pets!
                  .Where(p => p.PetId == id)
                  .Include(p => p.Category)
                  .Include(p => p.Comments!.OrderByDescending(c => c.CreatedDate))
                  .FirstOrDefault();
         }

        public IEnumerable<Pet> GetPetByCategory(int category)
        {
            return _context.Pets!
               .Include(p => p.Category)
               .Where(c => c.CategoryId == category);
        }

        public IEnumerable<Pet> GetPetsByLikes(int counter)
        {
            return _context.Pets!
                .OrderByDescending(a => a.Like)
                .Take(counter)
                .OrderBy(a => a.PetId)
                .Include(p => p.Category);
        }

        public Pet? AddPetLike(int id)
        {
            var pet = _context.Pets!.FirstOrDefault(p => p.PetId == id);
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
            var pet = GetPetByIDAndComments(comment.PetId);
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
