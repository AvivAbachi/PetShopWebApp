using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IRepository, MyRepository>();
string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<PetShopConetex>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PetShopConetex>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
