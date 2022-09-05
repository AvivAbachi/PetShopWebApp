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
        public void GetPetsTest()
        {
            var pets = publicRepository.GetPets().ToArray();
            CollectionAssert.AllItemsAreInstancesOfType(pets, typeof(Pet));
        }

        [TestMethod]
        public void AddPetLikeTest()
        {
            var pet = publicRepository.GetPetByIDAndComments(1);
            if (pet == null) throw new AssertFailedException("");
            int like = pet.Like;
            publicRepository.AddPetLike(1);
            Assert.IsTrue(like + 1 == pet.Like);
        }

        [TestMethod]
        public void GetPetByCategoryTest()
        {
            int category = 1;
            var pets = publicRepository.GetPetByCategory(category);
            Assert.IsTrue(pets.All(a => a.CategoryId == category));
        }

        [TestMethod]
        public void GetPetsByLikesTest()
        {
            int count = 2;
            var pets = publicRepository.GetPetsByLikes(count).ToList();
            var allPets = publicRepository.GetPets().ToList();
            for (int i = 0; i < count; i++)
            {
                var topPet = allPets.MaxBy(p => p.Like);
                Assert.IsTrue(pets[i].PetId == topPet?.PetId);
                allPets.Remove(topPet);
            }
        }

        [TestMethod]
        public void GetPetByIDAndCommentsTest()
        {
            int id = 1;
            var petComment = publicRepository.GetPetByIDAndComments(id)
                ?.Comments!.OrderBy(c => c.CommentId).ToList();
            var comments = publicRepository.GetComments()
                .Where(c => c.PetId == id).OrderBy(c => c.CommentId).ToList();
            CollectionAssert.AreEqual(comments, petComment);
        }

        [TestMethod]
        public void AddAnimaCommentTest()
        {
            int id = 2;
            string auther = "Amit";
            string text = "Testing adding comment";
            publicRepository.AddPetComment(id, auther, text);
            var comment = publicRepository.GetComments().Last();
            Assert.IsTrue(comment.PetId == id && comment.Auther == auther && comment.Text == text);
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