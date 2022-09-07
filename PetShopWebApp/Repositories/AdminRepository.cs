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

        public bool EditPet(Pet editPet)
        {
            var pet = _context.Pets!.FirstOrDefault(p => p.PetId == editPet.PetId);
            if (pet != null)
            {
                pet.Name = editPet.Name;
                pet.Description = editPet.Description;
                pet.Age = editPet.Age;
                pet.CategoryId = editPet.CategoryId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Pet?> UploadPicture(int id, IFormFile image)
        {
            var pet = _context.Pets!.FirstOrDefault(p => p.PetId == id);
            if (pet != null)
            {
                if (pet.PictureURL != null)
                {
                    string oldPath = Path.Combine(_environment.WebRootPath, pet.PictureURL.Remove(0, 1).Replace("/", "\\"));
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }
                string fileName = pet.PetId + Path.GetExtension(image.FileName);
                string targetPath = Path.Combine(_environment.WebRootPath, "upload", fileName);
                using (var fs = File.Create(targetPath))
                {
                    await image.CopyToAsync(fs);
                }
                pet!.PictureURL = $"/upload/{fileName}";
                _context.SaveChanges();
            }
            return pet;
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
