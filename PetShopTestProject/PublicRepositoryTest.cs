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
        public void GetAnimalsTest()
        {
            var pets = _publicRepository.GetAnimals().ToArray();
            CollectionAssert.AllItemsAreInstancesOfType(pets, typeof(Animal));
        }

        [TestMethod]
        public void AddAnimalLikeTest()
        {
            var pet = _publicRepository.GetAnimalByIDAndComments(1);
            if (pet == null) throw new AssertFailedException("");
            int like = pet.Like;
            _publicRepository.AddAnimalLike(1);
            Assert.IsTrue(like + 1 == pet.Like);
        }

        [TestMethod]
        public void GetAnimalByCategoryTest()
        {
            int category = 1;
            var pets = _publicRepository.GetAnimalByCategory(category);
            Assert.IsTrue(pets.All(a => a.CategoryId == category));
        }

        [TestMethod]
        public void GetAnimalsByLikesTest()
        {
            int count = 2;
            var pets = _publicRepository.GetAnimalsByLikes(count).ToList();
            var allPets = _publicRepository.GetAnimals().ToList();
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
            var comments = _publicRepository
                .GetAnimalByIDAndComments(id)
                ?.Comments?.ToList();
            CollectionAssert.AllItemsAreNotNull(comments);
        }

        [TestMethod]
        public void AddAnimaCommentTest()
        {
            int id = 2;
            string auther = "Amit";
            string text = "Testing adding comment";
            var comment = new Comment { AnimalId = id, Auther = auther, Text = text };
            bool succsses = _publicRepository.AddAnimaComment(comment);
            Assert.IsTrue(succsses);
            var pet = _publicRepository.GetAnimalByIDAndComments(id);
            var lastComment = pet?.Comments?.Last();
            Assert.IsTrue(lastComment?.AnimalId == id && lastComment?.Auther == auther && lastComment?.Text == text);
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