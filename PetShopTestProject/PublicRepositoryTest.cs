using Microsoft.Extensions.DependencyInjection;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;

namespace PetShopTestProject
{
    [TestClass]
    public class PublicRepositoryTest : PetShopInitializer
    {
        private readonly IPublicRepository publicRepository;
        public PublicRepositoryTest()
        {
            publicRepository = App.Services.GetRequiredService<IPublicRepository>();
        }

        [TestMethod]
        public void GetAnimalsTest()
        {
            var pets = publicRepository.GetAnimals().ToArray();
            CollectionAssert.AllItemsAreInstancesOfType(pets, typeof(Animal));
        }

        [TestMethod]
        public void AddAnimalLikeTest()
        {
            var pet = publicRepository.GetAnimalByIDAndComments(1);
            int like = pet.Like;
            publicRepository.AddAnimalLike(1);
            Assert.IsTrue(like + 1 == pet.Like);
        }
    }
}