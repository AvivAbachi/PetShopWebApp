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
        public async Task AddAnimal(Animal animal)
        {
            if (animal.File != null) animal.PictureURL = await UploadPicture(animal.File, animal.AnimalId);
            _context.Animals!.Add(animal);
            _context.SaveChanges();
        }
        public async Task<bool> EditAnimal(Animal animal)
        {
            var pet = _context.Animals!.FirstOrDefault(p => p.AnimalId == animal.AnimalId);
            if (pet == null) return false;
            pet.Name = animal.Name;
            pet.Description = animal.Description;
            pet.Age = animal.Age;
            pet.CategoryId = animal.CategoryId;
            if (animal.File != null) pet.PictureURL = await UploadPicture(animal.File, pet.AnimalId);
            _context.SaveChanges();
            return true;
        }
        private async Task<string> UploadPicture(IFormFile file, int id)
        {
            //if (!file!.ContentType.StartsWith("image/")) throw new AggregateException("file type not vaild");
            string FilePath = Path.Combine(_environment.WebRootPath, "upload");
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            var fileName = $"{id}.{file!.ContentType.Split('/')[1]}";
            var filePath = Path.Combine(FilePath, fileName);

            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
            }
            return filePath;
        }
        public bool RemoveAnimal(int id)
        {
            var pet = _context.Animals!
                  .Where(p => p.AnimalId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet == null) return false;
            _context.Comments!.RemoveRange(pet.Comments!);
            _context.Animals!.Remove(pet);
            _context.SaveChanges();
            return true;
        }
    }
}
