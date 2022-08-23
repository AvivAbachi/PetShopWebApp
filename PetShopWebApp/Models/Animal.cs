namespace PetShopWebApp.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string? Name { get; set; }
        public float Age { get; set; }
        public string? PictureURL { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
    }
}
