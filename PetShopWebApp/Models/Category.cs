namespace PetShopWebApp.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string? Name { get; set; }
		public virtual ICollection<Pet>? Pets { get; set; }
	}
}
