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

        public async Task AddPet(Pet pet)
        {
            _context.Pets!.Add(pet);
            _context.SaveChanges();
            if (pet.File != null)
            {
                pet.PictureURL = await UploadPicture(pet.File, pet.PetId);
                _context.SaveChanges();
            }
        }

        public async Task<bool> EditPet(Pet pet)
        {
            var petToEdit = _context.Pets!.FirstOrDefault(p => p.PetId == pet.PetId);
            if (petToEdit != null)
            {
                petToEdit.Name = pet.Name;
                petToEdit.Description = pet.Description;
                petToEdit.Age = pet.Age;
                petToEdit.CategoryId = pet.CategoryId;
                if (pet.File != null)
                {
                    if (File.Exists(petToEdit?.PictureURL)) File.Delete(petToEdit.PictureURL!);
                    petToEdit!.PictureURL = await UploadPicture(pet.File, petToEdit.PetId);
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<string> UploadPicture(IFormFile file, int id)
        {
            string FilePath = Path.Combine(_environment.WebRootPath, "upload");
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
            string fileName = $"{id}.{file!.FileName.Split('.')[1]}";
            var filePath = Path.Combine(FilePath, fileName);
            using (var fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
            }
            return $"/upload/{fileName}";
        }

        public bool RemovePet(int id)
        {
            var pet = _context.Pets!
                  .Where(p => p.PetId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                var targetPath = _environment.WebRootPath + pet.PictureURL!.Replace("/", "\\");
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
