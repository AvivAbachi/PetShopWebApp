using System.ComponentModel.DataAnnotations;

namespace PetShopWebApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(100)]
        public string? Auther { get; set; }
        [Required(ErrorMessage = "Please enter your comment")]
        public string? Text { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
