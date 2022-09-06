using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShopWebApp.Data;
using PetShopWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<PetShopConetex>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PetShopConetex>();
builder.Services.AddTransient<IPublicRepository, PublicRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PetShopConetex>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/404");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.Run();
