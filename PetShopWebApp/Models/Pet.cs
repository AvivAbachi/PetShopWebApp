using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApp.Models
{
    public class Pet
    {
        public int PetId { get; set; }

        [Required(ErrorMessage = "Please enter Pet Name")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter Pet Age")]
        [Range(0, 150.00, ErrorMessage = "Please enter Pet Age in range 0-150")]
        public double Age { get; set; }
        public int Like { get; set; }

        public string? PictureURL { get; set; }

        [Required(ErrorMessage = "Please enter Pet Descriotion")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please choose category")]
        public int CategoryId { get; set; }

        [NotMapped]
        [FileTypeValidation]
        public IFormFile? File { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
