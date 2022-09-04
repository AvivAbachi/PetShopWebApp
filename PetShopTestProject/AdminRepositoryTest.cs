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
        public void AddAnimalTest()
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string url = Path.GetFullPath(Path.Combine(fileInfo.DirectoryName!, @"..\..\..\Sample.jpg"));
            IFormFile file;
            using (var stream = File.OpenRead(url))
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
                PictureURL = url,
                CategoryId = 1,
                //File = file,
            };
            adminRipository.AddAnimal(animal);
            int countAfterAdd = publicRipository.GetAnimals().Count();
            Assert.IsTrue(countBeforeAdd + 1 == countAfterAdd);
            if (countBeforeAdd + 1 == countAfterAdd) adminRipository.RemoveAnimal(animal.AnimalId);
        }

        [TestMethod]
        public void EditAnimalTest()
        {
            int id = 1;
            var petBeforeEdit = publicRipository.GetAnimalByIDAndComments(id);
            petBeforeEdit!.Name = petBeforeEdit!.Name;
            petBeforeEdit.Description = petBeforeEdit.Description;
            petBeforeEdit.Age = petBeforeEdit.Age;
            petBeforeEdit.PictureURL = petBeforeEdit.PictureURL;
            petBeforeEdit.CategoryId = id;

            var petAfterEdit = publicRipository.GetAnimalByIDAndComments(id);
            petAfterEdit!.Name = "New Dog";
            petAfterEdit!.Description = "Dog after edit";
            petAfterEdit!.Age = 5;
            petAfterEdit!.CategoryId = 1;

            adminRipository.EditAnimal(petAfterEdit!);

            Assert.AreNotEqual(petBeforeEdit.Name, petAfterEdit.Name);

            adminRipository.EditAnimal(petBeforeEdit!);
        }

        [TestMethod]
        public void RemoveAnimalTest()
        {
            var pet = publicRipository.GetAnimals().Last();
            int countBeforeRemove = publicRipository.GetAnimals().Count();
            adminRipository.RemoveAnimal(pet.AnimalId);
            int countAfterRemove = publicRipository.GetAnimals().Count();

            Assert.IsTrue(countBeforeRemove == countAfterRemove + 1);
            Assert.IsNull(publicRipository.GetAnimalByIDAndComments(pet.AnimalId));

            if (countBeforeRemove == countAfterRemove + 1) adminRipository.AddAnimal(pet);
        }

        //bool Login(User user);

    }
}
