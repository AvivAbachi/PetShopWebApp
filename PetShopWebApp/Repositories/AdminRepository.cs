using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly PetShopConetex _context;
        private readonly IWebHostEnvironment _environment;

        public AdminRepository(PetShopConetex context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public Animal AddAnimal(Animal pet)
        {
            _context.Animals!.Add(pet);
            _context.SaveChanges();
            return pet;
        }

        public Animal? EditAnimal(Animal editPet)
        {
            var pet = _context.Animals!.FirstOrDefault(p => p.AnimalId == editPet.AnimalId);
            if (pet != null)
            {
                pet.Name = editPet.Name;
                pet.Description = editPet.Description;
                pet.Age = editPet.Age;
                pet.CategoryId = editPet.CategoryId;
                _context.SaveChanges();
                return pet;
            }
            return null;
        }

        public async Task UploadPicture(Animal pet)
        {
            string FilePath = Path.Combine(_environment.WebRootPath, "upload");
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            string fileName = $"{pet.AnimalId}.{pet.File!.FileName.Split('.')[1]}";
            var filePath = Path.Combine(FilePath, fileName);
            using (var fs = File.Create(filePath))
            {
                await pet.File.CopyToAsync(fs);
            }
            pet.PictureURL = $"/upload/{fileName}";
            pet.File = null;
            _context.SaveChanges();
        }

        public bool RemoveAnimal(int id)
        {
            var pet = _context.Animals!
                  .Where(p => p.AnimalId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                var targetPath = _environment.WebRootPath + pet.PictureURL!.Replace("/", "\\");
                if (File.Exists(targetPath)) File.Delete(targetPath!);
                _context.Comments!.RemoveRange(pet.Comments!);
                _context.Animals!.Remove(pet);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
