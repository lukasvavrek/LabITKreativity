using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkladUcebnic.Data;
using SkladUcebnic.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SkladUcebnicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkladUcebnicContext") ?? throw new InvalidOperationException("Connection string 'SkladUcebnicContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await Database.Migrate(services.GetRequiredService<SkladUcebnicContext>());
    
    SeedData.Initialize(services);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
