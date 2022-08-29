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
            int category = 1;
            var pets = publicRepository.GetAnimalByCategory(category);
            Assert.IsTrue(pets.All(a => a.CategoryId == category));
        }

        [TestMethod]
        public void GetAnimalsByLikesTest()
        {
            int count = 2;
            var pets = publicRepository.GetAnimalsByLikes(count).ToList();
            var allPets = publicRepository.GetAnimals().ToList();
            for (int i = 0; i < count; i++)
            {
                var topPet = allPets.MaxBy(p => p.Like);
                Assert.IsTrue(pets[i].AnimalId == topPet?.AnimalId);
                allPets.Remove(topPet);
            }
        }

        [TestMethod]
        public void GetAnimalByIDAndCommentsTest()
        {
            int id = 1;
            var petComment = publicRepository.GetAnimalByIDAndComments(id)
                .Comments!.OrderBy(c => c.CommentId).ToList();
            var comments = publicRepository.GetComments()
                .Where(c => c.AnimalId == id).OrderBy(c => c.CommentId).ToList();
            CollectionAssert.AreEqual(comments, petComment);
        }

        [TestMethod]
        public void AddAnimaCommentTest()
        {
            int id = 2;
            string auther = "Amit";
            string text = "Testing adding comment";
            publicRepository.AddAnimaComment(id, auther, text);
            var comment = publicRepository.GetComments().Last();
            Assert.IsTrue(comment.AnimalId == id && comment.Auther == auther && comment.Text == text);
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