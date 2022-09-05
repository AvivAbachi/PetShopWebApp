using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApp.Models
{
	public class Animal
	{
		public int AnimalId { get; set; }

		[Required(ErrorMessage ="Please enter Animal Name")]
		[MaxLength(100)]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Please enter Animal Age")]
		[Range(0,150.00,ErrorMessage ="Please enter Animal Age in range 0-150")]
		public double Age { get; set; }
		public int Like { get; set; }

		[Required(ErrorMessage = "Please enter Animal Picture")]
		public string? PictureURL { get; set; }

		[Required(ErrorMessage = "Please enter Animal Descriotion")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Please choose category")]
		public int CategoryId { get; set; }

		[NotMapped]
		public IFormFile? File { get; set; }
		public virtual Category? Category { get; set; }
		public virtual ICollection<Comment>? Comments { get; set; }
	}
}
