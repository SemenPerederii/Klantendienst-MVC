using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PrulariaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("PrulariaComConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)),
        x => x.MigrationsAssembly("KlantenDienstData")
    )
);


//Repositories
builder.Services.AddScoped<IArtikelRepository, SqlArtikelRepository>();
builder.Services.AddScoped<ArtikelService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
