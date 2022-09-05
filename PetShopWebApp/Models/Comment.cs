namespace PetShopWebApp.Models
{
	public class Comment
	{
		public int CommentId { get; set; }
		public int AnimalId { get; set; }
		public virtual Animal? Animal { get; set; }
		public string? Text { get; set; }
		public string? Auther { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
