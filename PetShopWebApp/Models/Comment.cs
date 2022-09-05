namespace PetShopWebApp.Models
{
	public class Comment
	{
		public int CommentId { get; set; }
		public int PetId { get; set; }
		public virtual Pet? Pet { get; set; }
		public string? Text { get; set; }
		public string? Auther { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
