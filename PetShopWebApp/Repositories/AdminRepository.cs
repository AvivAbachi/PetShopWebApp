using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;


namespace PetShopWebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        readonly PetShopConetex _context;
        private IWebHostEnvironment _environment;
        public AdminRepository(PetShopConetex context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public bool Login(User user)
        {
            var pet = _context.Users!
                .First(u => u.UserName == user.UserName);
            return pet.Password == user.Password;
        }
        public async void AddAnimal(Animal animal)
        {
            string? url = animal.File != null ? await UploadPicture(animal.File) : null;
            _context.Animals!.Add(new Animal
            {
                Name = animal.Name,
                Description = animal.Description,
                Age = animal.Age,
                PictureURL =url,
                CategoryId = animal.CategoryId,
            });
            _context.SaveChanges();
        }
        public async void EditAnimal(Animal animal)
        {
            var pet = _context.Animals!.First(p => p.AnimalId == animal.AnimalId);
            pet.Name = animal.Name;
            pet.Description = animal.Description;
            pet.Age = animal.Age;
            pet.PictureURL = animal.File != null ?await UploadPicture(animal.File) : pet.PictureURL;
            pet.CategoryId = animal.CategoryId;
            _context.SaveChanges();
        }
        private async Task< string?> UploadPicture(IFormFile file)
        {
            var FileDic = "img";
            string FilePath = Path.Combine(_environment.WebRootPath, FileDic);
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            var fileName = file!.FileName;
            var filePath = Path.Combine(FilePath, fileName);

            using (FileStream fs = File.Create(filePath))
            {
             await   file.CopyToAsync(fs);
            }
            return Path.GetFullPath(filePath);
        }

        public void RemoveAnimal(int id)
        {
            var pet = _context.Animals!
                  .Where(p => p.AnimalId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                if (pet.PictureURL != null) File.Delete(pet.PictureURL);
                _context.Comments!.RemoveRange(pet.Comments!);
                _context.Animals!.Remove(pet);
                _context.SaveChanges();
            }
        }
    }
}
