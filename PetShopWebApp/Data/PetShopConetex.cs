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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
