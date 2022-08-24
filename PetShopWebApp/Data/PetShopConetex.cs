using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Models;

namespace PetShopWebApp.Data
{
    public class PetShopConetex : DbContext
    {
        public PetShopConetex(DbContextOptions<PetShopConetex> options) : base(options) { }
        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<User>? Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Mammalia" },
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
                    Description = "The Golden Retriever is a Scottish breed of retriever dog of medium size. It is characterised by a gentle and affectionate nature and a striking golden coat. It is commonly kept as a pet and is among the most frequently registered breeds in several Western countries.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/b/bd/Golden_Retriever_Dukedestiny01_drvd.jpg"
                },
                new Animal
                {
                    AnimalId = 2,
                    Name = "Siberian Husky",
                    CategoryId = 1,
                    Age = 7.4,
                    Description = "The Siberian Husky is a medium-sized working sled dog breed. The breed belongs to the Spitz genetic family. It is recognizable by its thickly furred double coat, erect triangular ears, and distinctive markings, and is smaller than the similar-looking Alaskan Malamute.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/8/8b/Husky_L.jpg"
                },
                new Animal
                {
                    AnimalId = 3,
                    Name = "Border Collie",
                    CategoryId = 1,
                    Age = 5.2,
                    Description = "The Border Collie is a British breed of herding dog of medium size. They are descended from landrace sheepdogs once found all over the British Isles, but became standardized in the Anglo-Scottish border region. Presently they are used primarily as working dogs to herd livestock, specifically sheep.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/e/e4/Border_Collie_600.jpg"
                },
                new Animal
                {
                    AnimalId = 4,
                    Name = "Ragdoll",
                    CategoryId = 1,
                    Age = 8.6,
                    Description = "The Ragdoll is a cat breed with a colorpoint coat and blue eyes. Its morphology is large and weighty, and it has a semi-long and silky soft coat. Ragdolls were bred by American breeder Ann Baker in the 1960s. They are best known for their docile, placid, temperament and affectionate nature",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/6/64/Ragdoll_from_Gatil_Ragbelas.jpg"
                },
                new Animal
                {
                    AnimalId = 5,
                    Name = "Persian Cat",
                    CategoryId = 1,
                    Age = 8.6,
                    Description = "The Persian cat is a long-haired breed of cat characterized by its round face and short muzzle. It is also known as the \"Persian Longhair\" in English-speaking countries. The first documented ancestors of Persian cats were imported into Italy from Persia around 1620",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/1/15/White_Persian_Cat.jpg"
                },
                new Animal
                {
                    AnimalId = 6,
                    Name = "Rabbit",
                    CategoryId = 1,
                    Age = 8.6,
                    Description = "Rabbits, also known as bunnies or bunny rabbits, are small mammals in the family Leporidae of the order Lagomorpha. Oryctolagus cuniculus includes the European rabbit species and its descendants, the world's 305 breeds of domestic rabbit. Sylvilagus includes 13 wild rabbit species, among them the seven types of cottontail.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Oryctolagus_cuniculus_Rcdo.jpg/330px-Oryctolagus_cuniculus_Rcdo.jpg"
                },
                new Animal
                {
                    AnimalId = 7,
                    Name = "Macaw",
                    CategoryId = 2,
                    Age = 2.3,
                    Description = "Macaws are a group of New World parrots that are long-tailed and often colorful. They are popular in aviculture or as companion parrots, although there are conservation concerns about several species in the wild.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Blue-and-Yellow-Macaw.jpg/800px-Blue-and-Yellow-Macaw.jpg"
                },
                new Animal
                {
                    AnimalId = 8,
                    Name = "African Grey",
                    CategoryId = 2,
                    Age = 5.6,
                    Description = "The African grey parrot is an Old World parrot in the family Psittacidae. This article describes the Congo African grey parrot. The Timneh parrot ' was earlier treated as a subspecies but has since been elevated to a full species.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/2/28/Psittacus_erithacus_-perching_on_tray-8d.jpg"
                },

                new Animal
                {
                    AnimalId = 9,
                    Name = "Chameleon",
                    CategoryId = 3,
                    Age = 5.6,
                    Description = "hameleons or chamaeleons are a distinctive and highly specialized clade of Old World lizards with 202 species described as of June 2015. The members of this family are most known for their distinct range of colors as they are able to shift in different hues and brightness.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/c/c4/Chameleon_in_Berenty_Madagascar_0001.JPG"
                },
                new Animal
                {
                    AnimalId = 10,
                    Name = "Hermann's tortoise",
                    CategoryId = 3,
                    Age = 5.6,
                    Description = "Hermann's tortoise is a species of tortoise. Two subspecies are known: the western Hermann's tortoise and the eastern Hermann's tortoise. Sometimes mentioned as a subspecies, T. h. peleponnesica is not yet confirmed to be genetically different from T. h. boettgeri.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/6/6c/Testudo_hermanni_hermanni_Mallorca_02.jpg"
                },
                new Animal
                {
                    AnimalId = 11,
                    Name = "Goldfish",
                    CategoryId = 4,
                    Age = 1.2,
                    Description = "The goldfish is a freshwater fish in the family Cyprinidae of order Cypriniformes. It is commonly kept as a pet in indoor aquariums, and is one of the most popular aquarium fish. Goldfish released into the wild have become an invasive pest in parts of North America.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/a/ae/Katri.jpg"
                },
                new Animal
                {
                    AnimalId = 12,
                    Name = "Clownfish",
                    CategoryId = 4,
                    Age = 0.6,
                    Description = "Clownfish or anemonefish are fishes from the subfamily Amphiprioninae in the family Pomacentridae. Thirty species are recognized: one in the genus Premnas, while the remaining are in the genus Amphiprion. In the wild, they all form symbiotic mutualisms with sea anemones.",
                    PictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/Amphiprion_ocellaris_%28Clown_anemonefish%29_by_Nick_Hobgood.jpg/1920px-Amphiprion_ocellaris_%28Clown_anemonefish%29_by_Nick_Hobgood.jpg"
                });
            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, AnimalId = 1, Auther = "Aviv", Text = "Cute Dog", CreatedDate = DateTime.Now }
           );
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "admin", Password = "1234" }
           );
        }
    }
}
