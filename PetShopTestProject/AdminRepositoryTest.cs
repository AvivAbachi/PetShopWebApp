using Microsoft.AspNetCore.Http;

namespace PetShopTestProject
{
    [TestClass]
    public class AdminRepositoryTest : PetShopInitializer
    {
        private readonly IAdminRepository _adminRipository;
        private readonly IPublicRepository _publicRipository;
        public AdminRepositoryTest()
        {
            _adminRipository = App.Services.GetRequiredService<IAdminRepository>();
            _publicRipository = App.Services.GetRequiredService<IPublicRepository>();
        }

        [TestMethod]
        public async Task AddPetTest()
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

            await _adminRipository.AddPet(pet);

            Assert.IsTrue(saveCount + 1 == pets.Count());
            Assert.IsTrue(File.Exists(pet.PictureURL));
        }

        [TestMethod]
        public void EditPetTest()
        {
            int id = 1;
            var pet = _publicRipository.GetPetByIDAndComments(id);
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

        /// <summary>
        /// "App.Environment.WebRootPath" כתובת של התיקה הראשית
        /// "SimpleImage" הכתובת של התמונה
        /// 
        /// "formFile" א.להתחל את משנה
        /// !אבל הוא לא טוב "AddAnimalTest()"בגדול יש אתחול ב
        ///  ליפול UploadPicture הוא גורם ל
        /// 
        /// "UploadPicture(formFile, id)" ב.להריץ את פונקציה
        /// 
        /// ג.לבדוק עם התמונה נוספה
        /// 
        /// c#יש קליסים ב
        /// Directory, Path, File
        /// </summary>
        public async Task UploadImageTest()
        {
            //int id = 1;
            IFormFile formFile;

            using (var stream = File.OpenRead(SimpleImage))
            {
                file = new FormFile(stream, 0, stream.Length, "", stream.Name)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpg"
                };
            }
            //await adminRipository.UploadPicture(formFile, id);
        }

        [TestMethod]
        public void RemovePetTest()
        {
            var petlist = _publicRipository.GetPets();
            var pet = petlist.Last();
            int countBeforeRemove = petlist.Count();
            _adminRipository.RemovePet(pet.PetId);
            int countAfterRemove = petlist.Count();

            Assert.IsTrue(countBeforeRemove == countAfterRemove + 1);
            Assert.IsNull(_publicRipository.GetPetByIDAndComments(pet.PetId));
        }
    }
}
