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
            _context.Animals!.Add(animal);
            _context.SaveChanges();
        }
        public Animal EditAnimal(Animal animal)
        {
            var pet = _context.Animals!
                .First(p => p.AnimalId == animal.AnimalId);
            UploadPicture();
            return pet;
                }
        void UploadPicture()
        {
            throw new NotImplementedException();
        }

        public void RemoveAnimal(int id)
        {   
            var pet= _context.Animals!
                .Single(p=>p.AnimalId==id);
            _context.Animals!.Remove(pet);
            _context.SaveChanges();
        }
    }
}
