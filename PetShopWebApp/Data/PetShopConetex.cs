using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Models;

namespace PetShopWebApp.Data
{
    public class PetShopConetex : IdentityDbContext
    {
        private readonly IWebHostEnvironment _environment;
        public PetShopConetex(DbContextOptions<PetShopConetex> options, IWebHostEnvironment environment) : base(options)
        {
            _environment = environment;
        }
        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Category>? Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (_environment.ApplicationName != "testhost") ResetFolders();

            // Create Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            var admin = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Administrator",
                NormalizedUserName = "admin",
                SecurityStamp = string.Empty,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            admin.PasswordHash = hasher.HashPassword(admin, "12345678");
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Mammals" },
                new Category { CategoryId = 2, Name = "Birds" },
                new Category { CategoryId = 3, Name = "Reptilias" },
                new Category { CategoryId = 4, Name = "Fishs" }
            );

            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    AnimalId = 1,
                    Name = "Golden Retriever",
                    CategoryId = 1,
                    Age = 4.2,
                    Like = 5,
                    Description = "The Golden Retriever is a Scottish breed of retriever dog of medium size. It is characterised by a gentle and affectionate nature and a striking golden coat. It is commonly kept as a pet and is among the most frequently registered breeds in several Western countries.",
                    PictureURL = "/upload/1.jpg"
                },
                new Animal
                {
                    AnimalId = 2,
                    Name = "Siberian Husky",
                    CategoryId = 1,
                    Age = 7.4,
                    Like = 3,
                    Description = "The Siberian Husky is a medium-sized working sled dog breed. The breed belongs to the Spitz genetic family. It is recognizable by its thickly furred double coat, erect triangular ears, and distinctive markings, and is smaller than the similar-looking Alaskan Malamute.",
                    PictureURL = "/upload/2.jpg"
                },
                new Animal
                {
                    AnimalId = 3,
                    Name = "Border Collie",
                    CategoryId = 1,
                    Age = 5.2,
                    Like = 4,
                    Description = "The Border Collie is a British breed of herding dog of medium size. They are descended from landrace sheepdogs once found all over the British Isles, but became standardized in the Anglo-Scottish border region. Presently they are used primarily as working dogs to herd livestock, specifically sheep.",
                    PictureURL = "/upload/3.jpg"
                },
                new Animal
                {
                    AnimalId = 4,
                    Name = "Ragdoll",
                    Like = 5,
                    CategoryId = 1,
                    Age = 8.6,
                    Description = "The Ragdoll is a cat breed with a colorpoint coat and blue eyes. Its morphology is large and weighty, and it has a semi-long and silky soft coat. Ragdolls were bred by American breeder Ann Baker in the 1960s. They are best known for their docile, placid, temperament and affectionate nature",
                    PictureURL = "/upload/4.jpg"
                },
                new Animal
                {
                    AnimalId = 5,
                    Name = "Persian Cat",
                    CategoryId = 1,
                    Age = 8.6,
                    Description = "The Persian cat is a long-haired breed of cat characterized by its round face and short muzzle. It is also known as the \"Persian Longhair\" in English-speaking countries. The first documented ancestors of Persian cats were imported into Italy from Persia around 1620",
                    PictureURL = "/upload/5.jpg"
                },
                new Animal
                {
                    AnimalId = 6,
                    Name = "Rabbit",
                    CategoryId = 1,
                    Age = 8.6,
                    Description = "Rabbits, also known as bunnies or bunny rabbits, are small mammals in the family Leporidae of the order Lagomorpha. Oryctolagus cuniculus includes the European rabbit species and its descendants, the world's 305 breeds of domestic rabbit. Sylvilagus includes 13 wild rabbit species, among them the seven types of cottontail.",
                    PictureURL = "/upload/6.jpg"
                },
                new Animal
                {
                    AnimalId = 7,
                    Name = "Macaw",
                    CategoryId = 2,
                    Age = 2.3,
                    Description = "Macaws are a group of New World parrots that are long-tailed and often colorful. They are popular in aviculture or as companion parrots, although there are conservation concerns about several species in the wild.",
                    PictureURL = "/upload/7.jpg"
                },
                new Animal
                {
                    AnimalId = 8,
                    Name = "African Grey",
                    CategoryId = 2,
                    Age = 5.6,
                    Description = "The African grey parrot is an Old World parrot in the family Psittacidae. This article describes the Congo African grey parrot. The Timneh parrot ' was earlier treated as a subspecies but has since been elevated to a full species.",
                    PictureURL = "/upload/8.jpg"
                },
                new Animal
                {
                    AnimalId = 9,
                    Name = "Chameleon",
                    CategoryId = 3,
                    Age = 5.6,
                    Description = "hameleons or chamaeleons are a distinctive and highly specialized clade of Old World lizards with 202 species described as of June 2015. The members of this family are most known for their distinct range of colors as they are able to shift in different hues and brightness.",
                    PictureURL = "/upload/9.jpg"
                },
                new Animal
                {
                    AnimalId = 10,
                    Name = "Hermann's tortoise",
                    CategoryId = 3,
                    Age = 5.6,
                    Description = "Hermann's tortoise is a species of tortoise. Two subspecies are known: the western Hermann's tortoise and the eastern Hermann's tortoise. Sometimes mentioned as a subspecies, T. h. peleponnesica is not yet confirmed to be genetically different from T. h. boettgeri.",
                    PictureURL = "/upload/10.jpg"
                },
                new Animal
                {
                    AnimalId = 11,
                    Name = "Goldfish",
                    CategoryId = 4,
                    Age = 1.2,
                    Description = "The goldfish is a freshwater fish in the family Cyprinidae of order Cypriniformes. It is commonly kept as a pet in indoor aquariums, and is one of the most popular aquarium fish. Goldfish released into the wild have become an invasive pest in parts of North America.",
                    PictureURL = "/upload/11.jpg"
                },
                new Animal
                {
                    AnimalId = 12,
                    Name = "Clownfish",
                    CategoryId = 4,
                    Age = 0.6,
                    Description = "Clownfish or anemonefish are fishes from the subfamily Amphiprioninae in the family Pomacentridae. Thirty species are recognized: one in the genus Premnas, while the remaining are in the genus Amphiprion. In the wild, they all form symbiotic mutualisms with sea anemones.",
                    PictureURL = "/upload/12.jpg"
                });

            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, AnimalId = 1, Auther = "Shira", Text = "Small Dog", CreatedDate = DateTime.Now.AddHours(-10) },
                new Comment { CommentId = 2, AnimalId = 3, Auther = "Aviv", Text = "British Dog", CreatedDate = DateTime.Now.AddHours(-9.85) },
                new Comment { CommentId = 3, AnimalId = 11, Auther = "Aviv", Text = "Gold fish is very gold", CreatedDate = DateTime.Now.AddHours(-9.27) },
                new Comment { CommentId = 4, AnimalId = 3, Auther = "Shira", Text = "Good for sheep", CreatedDate = DateTime.Now.AddHours(-8.96) },
                new Comment { CommentId = 5, AnimalId = 7, Auther = "Aviv", Text = "Cute Macaw", CreatedDate = DateTime.Now.AddHours(-8.54) },
                new Comment { CommentId = 6, AnimalId = 9, Auther = "Shira", Text = "Small Chameleon", CreatedDate = DateTime.Now.AddHours(-8.12) },
                new Comment { CommentId = 7, AnimalId = 4, Auther = "Shira", Text = "Cat with blue eyes", CreatedDate = DateTime.Now.AddHours(-7.97) },
                new Comment { CommentId = 8, AnimalId = 5, Auther = "Aviv", Text = "Persian Cute Cat", CreatedDate = DateTime.Now.AddHours(-7.28) },
                new Comment { CommentId = 9, AnimalId = 10, Auther = "Aviv", Text = "Cute tortoise", CreatedDate = DateTime.Now.AddHours(-6.96) },
                new Comment { CommentId = 10, AnimalId = 5, Auther = "Shira", Text = "Persian's cates are with round face and short muzzle", CreatedDate = DateTime.Now.AddHours(-6.19) },
                new Comment { CommentId = 11, AnimalId = 2, Auther = "Shira", Text = "White black Dog", CreatedDate = DateTime.Now.AddHours(-5.64) },
                new Comment { CommentId = 12, AnimalId = 8, Auther = "Aviv", Text = "Cute African Grey", CreatedDate = DateTime.Now.AddHours(-5.57) },
                new Comment { CommentId = 13, AnimalId = 6, Auther = "Shira", Text = "Brown Rabbit", CreatedDate = DateTime.Now.AddHours(-5.12) },
                new Comment { CommentId = 14, AnimalId = 9, Auther = "Aviv", Text = "Cute Chameleon", CreatedDate = DateTime.Now.AddHours(-4.87) },
                new Comment { CommentId = 15, AnimalId = 1, Auther = "Aviv", Text = "Cute Dog", CreatedDate = DateTime.Now.AddHours(-4.33) },
                new Comment { CommentId = 16, AnimalId = 8, Auther = "Shira", Text = "African Grey is grey", CreatedDate = DateTime.Now.AddHours(-4.21) },
                new Comment { CommentId = 17, AnimalId = 12, Auther = "Shira", Text = "Clown fish cute", CreatedDate = DateTime.Now.AddHours(-3.84) },
                new Comment { CommentId = 18, AnimalId = 10, Auther = "Shira", Text = "Small tortoise", CreatedDate = DateTime.Now.AddHours(-3.4) },
                new Comment { CommentId = 19, AnimalId = 2, Auther = "Aviv", Text = "Snow Dog", CreatedDate = DateTime.Now.AddHours(-2.98) },
                new Comment { CommentId = 20, AnimalId = 4, Auther = "Aviv", Text = "Cute Cat", CreatedDate = DateTime.Now.AddHours(-2.65) },
                new Comment { CommentId = 21, AnimalId = 11, Auther = "Shira", Text = "Gold fish beautiful", CreatedDate = DateTime.Now.AddHours(-2.38) },
                new Comment { CommentId = 22, AnimalId = 6, Auther = "Aviv", Text = "Cute Rabbit", CreatedDate = DateTime.Now.AddHours(-1.74) },
                new Comment { CommentId = 23, AnimalId = 7, Auther = "Shira", Text = "beautiful Macaw", CreatedDate = DateTime.Now.AddHours(-0.93) },
                new Comment { CommentId = 24, AnimalId = 12, Auther = "Aviv", Text = "Clown fish nice", CreatedDate = DateTime.Now.AddHours(-0.05) }
           );
        }

        private void ResetFolders()
        {
            string sourcePath = Path.Combine(_environment.WebRootPath, "assets/pets/");
            string targetPath = Path.Combine(_environment.WebRootPath, "upload");
            if (Directory.Exists(targetPath)) Directory.Delete(targetPath, true);
            Directory.CreateDirectory(targetPath);
            foreach (string fullPath in Directory.GetFiles(sourcePath))
            {
                string fileName = Path.GetFileName(fullPath);
                File.Copy(fullPath, Path.Combine(targetPath, fileName));
            }
        }
    }
}
