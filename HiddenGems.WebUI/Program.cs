using HiddenGems.Application.Common.Repositories;
using HiddenGems.Application.Services.HiddenGems;
using HiddenGems.Infrastructure.Data;
using HiddenGems.Infrastructure.Identity;
using HiddenGems.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHiddenGemsService, HiddenGemsService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization(options => options.AddPolicy("AdminOnly", builder => builder.RequireClaim("IsAdmin")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
    if (userManager.FindByEmailAsync("admin@test.be").Result == null)
    {
        ApplicationUser adminUser = new ApplicationUser();
        adminUser.UserName = "admin@test.be";
        adminUser.Email = "admin@test.be";
        adminUser.EmailConfirmed = true;
        adminUser.FirstName = "Teo";
        adminUser.LastName = "Admin";
        var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
        if (result.Succeeded)
        {

            Claim[] claims = new Claim[] {
                new Claim("IsAdmin", string.Empty),
                new Claim(ClaimTypes.DateOfBirth, "03/12/1995")
            };

            userManager.AddClaimsAsync(adminUser, claims).Wait();
        }
    }

    if (userManager.FindByEmailAsync("user@test.be").Result == null)
    {
        ApplicationUser normalUser = new ApplicationUser();
        normalUser.UserName = "user@test.be";
        normalUser.Email = "user@test.be";
        normalUser.EmailConfirmed = true;
        normalUser.FirstName = "John";
        normalUser.LastName = "Wick";
        var result = userManager.CreateAsync(normalUser, "User123!").Result;
        if (result.Succeeded)
        {

            Claim[] claims = new Claim[] {
                new Claim(ClaimTypes.DateOfBirth, "26/06/2004")
            };

            userManager.AddClaimsAsync(normalUser, claims).Wait();
        }
    }
}

app.Run();
