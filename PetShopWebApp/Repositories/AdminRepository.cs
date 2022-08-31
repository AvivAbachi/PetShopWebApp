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
            _context.Animals!.Add(new Animal
            {
                Name = animal.Name,
                Description = animal.Description,
                Age = animal.Age,
                CategoryId = animal.CategoryId,
            });
            var r = _context.Animals!.Last();
            string url = animal.File != null ? (await UploadPicture(animal.File, r.AnimalId)) : "";
            await _context.SaveChangesAsync();
        }
        public async Task EditAnimal(Animal animal)
        {
            var pet = _context.Animals!.First(p => p.AnimalId == animal.AnimalId);
            pet.Name = animal.Name;
            pet.Description = animal.Description;
            pet.Age = animal.Age;
            if (animal.File != null) pet.PictureURL = await UploadPicture(animal.File, pet.AnimalId);
            pet.CategoryId = animal.CategoryId;
            await _context.SaveChangesAsync();
        }
        private async Task<string> UploadPicture(IFormFile file, int id)
        {
            string FilePath = Path.Combine(_environment.WebRootPath, "upload");
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            var fileName = $"{id}.{file!.ContentType.Split('/')[1]}";
            var filePath = Path.Combine(FilePath, fileName);

            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
            }
            return Path.Combine(_environment.WebRootPath, $"/upload/{fileName}");
        }
        public void RemoveAnimal(int id)
        {
            var pet = _context.Animals!
                  .Where(p => p.AnimalId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                //if (pet.PictureURL != null) File.Delete(pet.PictureURL);
                _context.Comments!.RemoveRange(pet.Comments!);
                _context.Animals!.Remove(pet);
                _context.SaveChanges();
            }
        }
    }
}
