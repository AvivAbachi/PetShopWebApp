using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Models;

namespace PetShopWebApp.Repositories
{
	public class PublicRepository : IPublicRepository
	{
		private readonly PetShopConetex _context;
		public PublicRepository(PetShopConetex context)
		{
			_context = context;
		}
		public IEnumerable<Pet> GetPets()
		{
			return _context.Pets!
				.OrderBy(a => a.PetId);
		}
		public IEnumerable<Comment> GetComments()
		{
			return _context.Comments!;
		}
		public Pet? GetPetByIDAndComments(int id)
		{
			var pet = _context.Pets!
				  .Where(p => p.PetId == id)
				  .Include(p => p.Category)
				  .Include(p => p.Comments!.OrderByDescending(c => c.CreatedDate))
				  .FirstOrDefault();
			return pet;
		}
		public IEnumerable<Pet> GetPetByCategory(int category)
		{
			return _context.Pets!
			   .Include(p => p.Category)
			   .Where(c => c.CategoryId == category);
		}
		public IEnumerable<Pet> GetPetsByLikes(int counter)
		{
			return _context.Pets!
				.OrderByDescending(a => a.Like)
				.Take(counter)
				.OrderBy(a => a.PetId)
				.Include(p => p.Category);
		}
		public int AddPetLike(int id)
		{
			var pet = _context.Pets!.First(p => p.PetId == id);
			pet.Like++;
			_context.SaveChanges();
			return pet.Like;
		}
		public Comment AddPetComment(int id, string auther, string text)
		{
			var pet = GetPetByIDAndComments(id);
			var comment = new Comment { PetId = id, Auther = auther, Text = text, CreatedDate = DateTime.Now };
			pet?.Comments!.Add(comment);
			_context.SaveChanges();
			return comment;
		}
		public IEnumerable<Category> GetCategories()
		{
			return _context.Category!;
		}
	}
}
