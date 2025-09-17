using Microsoft.EntityFrameworkCore;
using GoogLoginTest.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); // Habilita controladores MVC con soporte para vistas Razor.
builder.Services.AddScoped<IAuthService, AuthService>(); 
builder.Services.AddDbContext<LoginDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita archivos estáticos (css, js, imágenes) desde wwwroot.
app.MapControllers(); // Habilita el mapeo de rutas por atributos en los controladores.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();