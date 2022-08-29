using Microsoft.Extensions.DependencyInjection;
using PetShopWebApp.Models;
using PetShopWebApp.Repositories;
using System.Linq;

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

        [TestMethod]
        public void GetAnimalByCategoryTest()
        {
            var pets=publicRepository.GetAnimalByCategory(1).ToList();
            Assert.IsFalse(pets.First().CategoryId != 1);
        }

        [TestMethod]
        public void GetAnimalsByLikesTest()
        {
            var pets = publicRepository.GetAnimalsByLikes(2).ToList();

            var petMaxArray = new List<Animal>();
            var getPetMax1 = publicRepository.GetAnimals().MaxBy(p=>p.Like!);
            petMaxArray.Add(getPetMax1!);
            var tempPet = publicRepository.GetAnimalsByLikes(2).ToList();
            tempPet.Remove(getPetMax1!);
            petMaxArray.Add(tempPet.MaxBy(p => p.Like));
            Assert.IsTrue(Enumerable.SequenceEqual(pets.OrderBy(e => e.AnimalId), petMaxArray.OrderBy(e => e.AnimalId)));

        }

        [TestMethod]
        public void GetAnimalByIDAndCommentsTest()
        {
            var pets=publicRepository.GetAnimalByIDAndComments(1);
            Assert.IsTrue(publicRepository.GetAnimals().Any(a => a.AnimalId == 1));
        }

        [TestMethod]
        public void AddAnimaCommentTest()
        {
            publicRepository.AddAnimaComment(2,"Amit","Testing adding comment");
            var comments = publicRepository.GetComments();
            Assert.IsTrue(comments.Any(c => c.AnimalId == 2 && c.Auther == "Amit" && c.Text == "Testing adding comment"));
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            var categories = publicRepository.GetCategories().ToArray();
            CollectionAssert.AllItemsAreInstancesOfType(categories, typeof(Category));
            CollectionAssert.AllItemsAreUnique(categories);
        }
    }
}