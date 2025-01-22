using Microsoft.EntityFrameworkCore;
using SaadPortfolio.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SaadPortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SaadPortfolioContext") ?? throw new InvalidOperationException("Connection string 'SaadPortfolioContext' not found.")));

// Ajouter les services n�cessaires
builder.Services.AddControllersWithViews();

// Ajouter la configuration de la cha�ne de connexion et du DbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Le param�tre HSTS par d�faut est de 30 jours. Vous pouvez vouloir changer cela pour des sc�narios de production.
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
