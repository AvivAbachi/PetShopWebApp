using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace PetShopTestProject
{
    [TestClass]
    public class AdminRepositoryTest : PetShopInitializer
    {
        private readonly IAdminRepository adminRipository;
        private readonly IPublicRepository publicRipository;
        public AdminRepositoryTest()
        {
            adminRipository = App.Services.GetRequiredService<IAdminRepository>();
            publicRipository = App.Services.GetRequiredService<IPublicRepository>();
        }

        [TestMethod]
        public async Task AddAnimalTest()
        {
            IFormFile file;
            using (var stream = File.OpenRead(SimpleImage))
            {
                file = new FormFile(stream, 0, stream.Length, "", stream.Name)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpg"
                };
            }

            int countBeforeAdd = publicRipository.GetAnimals().Count();
            var animal = new Animal()
            {
                Name = "Dolphin",
                Description = "Dolphin loves to sweem",
                Age = 5,
                PictureURL = SimpleImage,
                CategoryId = 1,
                //File = file,
            };
            await adminRipository.AddAnimal(animal);
            int countAfterAdd = publicRipository.GetAnimals().Count();
            Assert.IsTrue(countBeforeAdd + 1 == countAfterAdd);
        }

        [TestMethod]
        public async Task EditAnimalTest()
        {
            int id = 1;
            var pet = publicRipository.GetAnimalByIDAndComments(id);
            var savePet = new Animal
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
            //pet!.PictureURL = url;

            await adminRipository.EditAnimal(pet);

            Assert.AreNotEqual(pet.Name, savePet.Name);
            Assert.AreNotEqual(pet.Description, savePet.Description);
            Assert.AreNotEqual(pet.Age, savePet.Age);
            Assert.AreNotEqual(pet.CategoryId, savePet.CategoryId);
            //Assert.AreNotEqual(pet.PictureURL, savePet.PictureURL);
        }

        /// <summary>
        /// "App.Environment.WebRootPath" כתובת של התיקה הראשית
        /// "SimpleImage" הכתובת של התמונה
        /// 
        /// "formFile" א.להתחל את משנה
        /// "AddAnimalTest()" עם את נתקעת את יכול לעזר ב
        /// 
        /// "UploadPicture(formFile, id)" ב.להריץ את פונקציה
        /// 
        /// ג.לבדוק עם התמונה נוספה
        /// 
        /// c# יש כלים ב
        /// Directory. Path. File.
        /// </summary>
        public async Task UploadImageTest()
        {
            //int id = 0;
            //IFormFile formFile;
            //await adminRipository.UploadPicture(formFile, id);
        }

        [TestMethod]
        public void RemoveAnimalTest()
        {
            var petlist = publicRipository.GetAnimals();
            var pet = petlist.Last();
            int countBeforeRemove = petlist.Count();
            adminRipository.RemoveAnimal(pet.AnimalId);
            int countAfterRemove = petlist.Count();

            Assert.IsTrue(countBeforeRemove == countAfterRemove + 1);
            Assert.IsNull(publicRipository.GetAnimalByIDAndComments(pet.AnimalId));
        }
    }
}
