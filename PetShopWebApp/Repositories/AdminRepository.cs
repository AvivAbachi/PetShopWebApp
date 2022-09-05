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
            if (pet.File != null) pet.PictureURL = await UploadPicture(pet.File, pet.PetId);
            _context.Pets!.Add(pet);
            _context.SaveChanges();
        }
        public async Task EditPet(Pet pet)
        {
            var petToEdit = _context.Pets!.First(p => p.PetId == pet.PetId);
            petToEdit.Name = pet.Name;
            petToEdit.Description = pet.Description;
            petToEdit.Age = pet.Age;
            if (pet.File != null) petToEdit.PictureURL = await UploadPicture(pet.File, petToEdit.PetId);
            petToEdit.CategoryId = pet.CategoryId;
            _context.SaveChanges();
        }
        private async Task<string> UploadPicture(IFormFile file, int id)
        {
            if (!file!.ContentType.StartsWith("image/")) throw new AggregateException("file type not vaild"); 
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
        public void RemovePet(int id)
        {
            var pet = _context.Pets!
                  .Where(p => p.PetId == id)
                  .Include(p => p.Comments)
                  .FirstOrDefault();
            if (pet != null)
            {
                //if (pet.PictureURL != null) File.Delete(pet.PictureURL);
                _context.Comments!.RemoveRange(pet.Comments!);
                _context.Pets!.Remove(pet);
                _context.SaveChanges();
            }
        }
    }
}
