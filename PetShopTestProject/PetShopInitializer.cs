using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetShopWebApp.Data;
using PetShopWebApp.Repositories;

namespace PetShopTestProject
{
    public abstract class PetShopInitializer
    {
        public WebApplication App { get; }
        public PetShopInitializer()
        {
            var builder = WebApplication.CreateBuilder();

            string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services.AddDbContext<PetShopConetex>(options => options.UseSqlServer(connectionString));
            builder.Services.AddTransient<IPublicRepository, PublicRepository>();
            builder.Services.AddTransient<IAdminRepository, AdminRepository>();

            var context = new DbContextOptionsBuilder<PetShopConetex>();

            App = builder.Build();

            using (var scope = App.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<PetShopConetex>();
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
        }
    }
}
