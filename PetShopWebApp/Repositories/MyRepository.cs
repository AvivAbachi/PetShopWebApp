using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public class MyRepository : IRepository
    {
        PetShopConetex _context;
        public MyRepository(PetShopConetex context)
        {
            _context = context;
        }
        public Animal GetAnimalByCategory(int category)
        {
            var petInDb = _context.Animals!
                .Where(c => c.CategoryId == category);
            //include??
            return (Animal)petInDb;
        }

        public Animal GetAnimalByIDAndComments(int Id)
        {
            var petInDb = _context.Animals!
                .Single(p => p.AnimalId == Id);

            return petInDb;
        }

        public IEnumerable<Animal> GetAnimals()
        {
            return _context.Animals!;
        }
    }
}
