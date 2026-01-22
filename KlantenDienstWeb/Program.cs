using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstServices;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

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


builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
builder.Services.AddScoped<ICategorieService, CategorieService>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IPersoneelslidRepository, PersoneelslidRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Localization
var supportedCultures = new[]
{
    new CultureInfo("nl-BE"),
    new CultureInfo("nl-NL")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("nl-BE");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // Zorgt dat browser-taal (Accept-Language) mee kan bepalen.
    options.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
});

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
app.UseSession();
app.UseRequestLocalization();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
