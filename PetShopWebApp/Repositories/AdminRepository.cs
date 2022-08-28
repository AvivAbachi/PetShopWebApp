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
        public void AddAnimal()
        {
            throw new NotImplementedException();
        }
        public void RemoveAnimal()
        {
            throw new NotImplementedException();
        }
        public void EditAnimal()
        {
            UploadPicture();
            throw new NotImplementedException();
        }
        void UploadPicture()
        {
            throw new NotImplementedException();
        }
    }
}
