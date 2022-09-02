using Microsoft.Extensions.DependencyInjection;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;

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
            int countBeforeAdd = publicRipository.GetAnimals().Count();
            var animal = new Animal()
            {
                Name = "Dolphin",
                Description = "Dolphin loves to sweem",
                Age = 5,
                PictureURL = "https://www.medpeople.org.il/wp-content/uploads/thumbs/dolphine_Easy-Resize.com_-37mcemwsr4u12kdc6vfuo0.jpg",
                CategoryId = 1,
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
            petAfterEdit!.PictureURL = "data:image/jpeg;base64,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";
            petAfterEdit!.CategoryId = 1;

            adminRipository.EditAnimal(petAfterEdit!);

            Assert.AreNotEqual(petBeforeEdit, petAfterEdit);

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
