using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        readonly PetShopConetex _context;
        public AdminRepository(PetShopConetex context)
        {
            _context = context;
        }
        public bool Login(User user)
        {
            var pet = _context.Users!
                .First(u => u.UserName == user.UserName);
            return pet.Password == user.Password;
        }
        public void AddAnimal(Animal animal)
        {
            _context.Animals!.Add(new Animal
            {
                Name = animal.Name,
                Description = animal.Description,
                Age = animal.Age,
                PictureURL = animal.PictureURL,
                CategoryId = animal.CategoryId,
            });
            _context.SaveChanges();
        }
        public Animal EditAnimal(Animal animal)
        {
            var pet = _context.Animals!.First(p => p.AnimalId == animal.AnimalId);
            pet.Name = animal.Name;
            pet.Description = animal.Description;
            pet.Age = animal.Age;
            pet.PictureURL = animal.PictureURL;
            pet.CategoryId = animal.CategoryId;
            _context.SaveChanges();
            //UploadPicture();
            return pet;
        }
        void UploadPicture()
        {
            throw new NotImplementedException();
        }

        public void RemoveAnimal(int id)
        {
            var pet = _context.Animals!
                  .Where(p => p.AnimalId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                _context.Comments!.RemoveRange(pet.Comments!);
                _context.Animals!.Remove(pet);
                _context.SaveChanges();
            }
        }
    }
}
