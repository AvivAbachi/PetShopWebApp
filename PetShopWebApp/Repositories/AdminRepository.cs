using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;
using System.Text.RegularExpressions;

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
        public bool Login(User user)
        {
            var pet = _context.Users!.First(u => u.UserName == user.UserName);
            return pet.Password == user.Password;
        }
        public void AddAnimal(Animal animal)
        {
            string url = animal.File != null ? UploadPicture(animal.File, animal.AnimalId) : "";
            _context.Animals!.Add(new Animal
            {
                Name = animal.Name,
                Description = animal.Description,
                Age = animal.Age,
                PictureURL = url,
                CategoryId = animal.CategoryId,
            });
            _context.SaveChanges();
        }
        public void EditAnimal(Animal animal)
        {
            var pet = _context.Animals!.First(p => p.AnimalId == animal.AnimalId);
            pet.Name = animal.Name;
            pet.Description = animal.Description;
            pet.Age = animal.Age;
            if (animal.File != null) pet.PictureURL = UploadPicture(animal.File, pet.AnimalId);
            pet.CategoryId = animal.CategoryId;
            _context.SaveChanges();
        }
        private string UploadPicture(IFormFile file, int id)
        {
            string FilePath = Path.Combine(_environment.WebRootPath, "upload");
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            var fileName = $"{id}.{file!.ContentType.Split('/')[1]}";
            var filePath = Path.Combine(FilePath, fileName);

            using (FileStream fs = File.Create(filePath))
            {
                file.CopyTo(fs);
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
