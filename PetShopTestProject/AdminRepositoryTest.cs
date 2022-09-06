using Microsoft.AspNetCore.Http;

namespace PetShopTestProject
{
    [TestClass]
    public class AdminRepositoryTest : PetShopInitializer
    {
        const int testId = 1;
        private readonly IAdminRepository _adminRipository;
        private readonly IPublicRepository _publicRipository;
        public AdminRepositoryTest()
        {
            _adminRipository = App.Services.GetRequiredService<IAdminRepository>();
            _publicRipository = App.Services.GetRequiredService<IPublicRepository>();
        }

        [TestMethod]
        public void AddPetTest()
        {
            var pets = _publicRipository.GetPets();
            int saveCount = pets.Count();
            var pet = new Pet()
            {
                Name = "Dolphin",
                Description = "Dolphin loves to sweem",
                Age = 5,
                PictureURL = SimpleImage,
                CategoryId = 1,
            };

            _adminRipository.AddPet(pet);

            Assert.IsTrue(saveCount + 1 == pets.Count());
            Assert.IsTrue(File.Exists(pet.PictureURL));
        }

        [TestMethod]
        public void EditPetTest()
        {
            var pet = _publicRipository.GetPetByIDAndComments(testId);
            var savePet = new Pet
            {
                Name = pet!.Name,
                Description = pet.Description,
                Age = pet.Age,
                CategoryId = pet.CategoryId,
            };

            pet!.Name = "New Dog";
            pet!.Description = "Dog after edit";
            pet!.Age = 10;
            pet!.CategoryId = 2;

            _adminRipository.EditPet(pet);

            Assert.AreNotEqual(pet.Name, savePet.Name);
            Assert.AreNotEqual(pet.Description, savePet.Description);
            Assert.AreNotEqual(pet.Age, savePet.Age);
            Assert.AreNotEqual(pet.CategoryId, savePet.CategoryId);
        }

        [TestMethod]
        public async Task UploadImageTest()
        {
            var pet = _publicRipository.GetPetByIDAndComments(testId);
            var file = await File.ReadAllBytesAsync(SimpleImage);
            var ms = new MemoryStream(file);
            var fileName = Path.GetFileName(SimpleImage);
            pet!.File = new FormFile(ms, 0, ms.Length, "File", fileName);
            await _adminRipository.UploadPicture(pet!);
            var path = pet.PictureURL!.Remove(0, 1).Replace("/", "\\");
            var fullPath = Path.Combine(App.Environment.WebRootPath, path);
            Assert.IsTrue(File.Exists(fullPath));
        }

        [TestMethod]
        public async Task RemovePetTest()
        {
            await UploadImageTest();
            var petlist = _publicRipository.GetPets();
            int saveCount = petlist.Count();
            string path = petlist.First(p => p.PetId == testId).PictureURL!.Remove(0, 1).Replace("/", "\\");
            string fullPath = Path.Combine(App.Environment.WebRootPath, path);
            _adminRipository.RemovePet(testId);
            Assert.IsTrue(saveCount == petlist.Count() + 1);
            Assert.IsNull(petlist.FirstOrDefault(p => p.PetId == testId));
            Assert.IsFalse(File.Exists(fullPath));
        }
    }
}
