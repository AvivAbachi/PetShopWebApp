using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;
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
        public void AddPetTest()
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

            int countBeforeAdd = publicRipository.GetPets().Count();
            var pet = new Pet()
            {
                Name = "Dolphin",
                Description = "Dolphin loves to sweem",
                Age = 5,
                PictureURL = url,
                CategoryId = 1,
                File = file,
            };
            adminRipository.AddPet(pet);
            int countAfterAdd = publicRipository.GetPets().Count();
            Assert.IsTrue(countBeforeAdd + 1 == countAfterAdd);
            if (countBeforeAdd + 1 == countAfterAdd) adminRipository.RemovePet(pet.PetId);
        }

        [TestMethod]
        public void EditPetTest()
        {
            int id = 1;
            var petBeforeEdit = publicRipository.GetPetByIDAndComments(id);
            petBeforeEdit!.Name = petBeforeEdit!.Name;
            petBeforeEdit.Description = petBeforeEdit.Description;
            petBeforeEdit.Age = petBeforeEdit.Age;
            petBeforeEdit.PictureURL = petBeforeEdit.PictureURL;
            petBeforeEdit.CategoryId = id;

            var petAfterEdit = publicRipository.GetPetByIDAndComments(id);
            petAfterEdit!.Name = "New Dog";
            petAfterEdit!.Description = "Dog after edit";
            petAfterEdit!.Age = 5;
            petAfterEdit!.CategoryId = 1;

            adminRipository.EditPet(petAfterEdit!);

            Assert.AreNotEqual(petBeforeEdit, petAfterEdit);

            adminRipository.EditPet(petBeforeEdit!);
        }

        [TestMethod]
        public void RemovePetTest()
        {
            var pet = publicRipository.GetPets().Last();
            int countBeforeRemove = publicRipository.GetPets().Count();
            adminRipository.RemovePet(pet.PetId);
            int countAfterRemove = publicRipository.GetPets().Count();

            Assert.IsTrue(countBeforeRemove == countAfterRemove + 1);
            Assert.IsNull(publicRipository.GetPetByIDAndComments(pet.PetId));

            if (countBeforeRemove == countAfterRemove + 1) adminRipository.AddPet(pet);
        }

    }
}
