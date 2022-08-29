using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<PetShopConetex>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IPublicRepository, PublicRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PetShopConetex>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

//if (env.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//   app.UseExceptionHandler("/Home/Error");
//}


app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
