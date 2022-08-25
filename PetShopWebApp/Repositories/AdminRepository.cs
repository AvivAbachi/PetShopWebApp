using PetShopWebApp.Data;

namespace PetShopWebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        readonly PetShopConetex _context;
        public AdminRepository(PetShopConetex context)
        {
            _context = context;
        }
        public bool Login()
        {
            throw new NotImplementedException();
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
