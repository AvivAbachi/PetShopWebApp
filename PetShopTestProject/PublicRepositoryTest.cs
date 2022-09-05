namespace PetShopTestProject
{
    [TestClass]
    public class PublicRepositoryTest : PetShopInitializer
    {
        private readonly IPublicRepository _publicRepository;
        public PublicRepositoryTest()
        {
            _publicRepository = App.Services.GetRequiredService<IPublicRepository>();
        }

        [TestMethod]
        public void GetPetsTest()
        {
            var pets = _publicRepository.GetPets().ToArray();
            CollectionAssert.AllItemsAreInstancesOfType(pets, typeof(Pet));
        }

        [TestMethod]
        public void AddPetLikeTest()
        {
            var pet = _publicRepository.GetPetByIDAndComments(1);
            if (pet == null) throw new AssertFailedException("");
            int like = pet.Like;
            _publicRepository.AddPetLike(1);
            Assert.IsTrue(like + 1 == pet.Like);
        }

        [TestMethod]
        public void GetPetByCategoryTest()
        {
            int category = 1;
            var pets = _publicRepository.GetPetByCategory(category);
            Assert.IsTrue(pets.All(a => a.CategoryId == category));
        }

        [TestMethod]
        public void GetPetsByLikesTest()
        {
            int count = 2;
            var pets = _publicRepository.GetPetsByLikes(count).ToList();
            var allPets = _publicRepository.GetPets().ToList();
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
            var comments = _publicRepository
                .GetPetByIDAndComments(id)
                ?.Comments?.ToList();
            CollectionAssert.AllItemsAreNotNull(comments);
        }

        [TestMethod]
        public void AddAnimaCommentTest()
        {
            int id = 2;
            string auther = "Amit";
            string text = "Testing adding comment";
            var comment = new Comment { PetId = id, Auther = auther, Text = text };
            bool succsses = _publicRepository.AddAnimaComment(comment);
            Assert.IsTrue(succsses);
            var pet = _publicRepository.GetPetByIDAndComments(id);
            var lastComment = pet?.Comments?.Last();
            Assert.IsTrue(lastComment?.PetId == id && lastComment?.Auther == auther && lastComment?.Text == text);
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            var categories = _publicRepository.GetCategories().ToArray();
            CollectionAssert.AllItemsAreInstancesOfType(categories, typeof(Category));
            CollectionAssert.AllItemsAreUnique(categories);
        }
    }
}