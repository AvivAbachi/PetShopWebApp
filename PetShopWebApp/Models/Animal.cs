﻿namespace PetShopWebApp.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string? Name { get; set; }
        public double Age { get; set; }
        public string? PictureURL { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
