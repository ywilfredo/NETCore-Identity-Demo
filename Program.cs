using IdentityUserDemo.Data;
using IdentityUserDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion
builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DefaultConnection"]);
});


// Configurar servicio de identity
builder.Services.AddIdentity<User, Role>(
    options =>
    {
        //Password
        options.Password.RequireDigit = false; //requiere 1 digito numerico
        options.Password.RequireLowercase = true; //requiere al menos 1 minuscula
        options.Password.RequireUppercase = true; //requiere al menos 1 mayuscula
        options.Password.RequireNonAlphanumeric = false; // requiere caracter especial
        options.Password.RequiredLength = 6; //requiere al menos 6 caracteres
        //Require Email confirmed
        options.SignIn.RequireConfirmedEmail = false; //requiere confirmar email la primera vez

    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DatabaseContext>();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
