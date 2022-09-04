using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace PetShopTestProject
{
    public abstract class PetShopInitializer
    {
        public WebApplication App { get; }
        public string SimpleImage { get; }

        public PetShopInitializer()
        {
            var builder = WebApplication.CreateBuilder();

            string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services.AddDbContext<PetShopConetex>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { options.User.RequireUniqueEmail = true; }).AddEntityFrameworkStores<PetShopConetex>();
            builder.Services.AddTransient<IPublicRepository, PublicRepository>();
            builder.Services.AddTransient<IAdminRepository, AdminRepository>();

            var context = new DbContextOptionsBuilder<PetShopConetex>();

            App = builder.Build();

            FileInfo fileInfo = new(Assembly.GetExecutingAssembly().Location);
            string path = Path.GetFullPath(Path.Combine(fileInfo.DirectoryName!, @"..\..\..\"));
            string uploadPath = Path.Combine(path, "upload");
            if (Directory.Exists(uploadPath)) Directory.Delete(uploadPath, true);
            Directory.CreateDirectory(uploadPath);

            App.Environment.WebRootPath = path;
            SimpleImage = Path.Combine(path, "Sample.jpg");

            using (var scope = App.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<PetShopConetex>();
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
        }
    }
}
