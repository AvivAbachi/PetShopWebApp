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

        public Pet AddPet(Pet pet)
        {
            _context.Pets!.Add(pet);
            _context.SaveChanges();
            return pet;
        }

        public Pet? EditPet(Pet editPet)
        {
            var pet = _context.Pets!.FirstOrDefault(p => p.PetId == editPet.PetId);
            if (pet != null)
            {
                pet.Name = editPet.Name;
                pet.Description = pet.Description;
                pet.Age = editPet.Age;
                pet.CategoryId = editPet.CategoryId;
                _context.SaveChanges();
                return pet;
            }
            return null;
        }

        public async Task UploadPicture(Pet pet)
        {
            string FilePath = Path.Combine(_environment.WebRootPath, "upload");
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            string fileName = pet.PetId + Path.GetExtension(pet.File!.FileName);
            var filePath = Path.Combine(FilePath, fileName);
            using (var fs = File.Create(filePath))
            {
                await pet.File.CopyToAsync(fs);
            }
            pet.PictureURL = $"/upload/{fileName}";
            pet.File = null;
            _context.SaveChanges();
        }

        public bool RemovePet(int id)
        {
            var pet = _context.Pets!
                  .Where(p => p.PetId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                var targetPath = Path.Combine(_environment.WebRootPath, pet.PictureURL!.Remove(0, 1).Replace("/", "\\"));
                if (File.Exists(targetPath)) File.Delete(targetPath!);
                _context.Comments!.RemoveRange(pet.Comments!);
                _context.Pets!.Remove(pet);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
