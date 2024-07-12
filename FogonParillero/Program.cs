using FogonParillero.Data;
using FogonParillero.Interfaces;
using FogonParillero.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();
builder.Services.AddScoped<ICategoriaProductoInterface, CategoriaProductoService>();
builder.Services.AddScoped<ICategoriaInsumoInterface, CategoriaInsumoService>();
builder.Services.AddScoped<IProductoInterface, ProductoService>();
builder.Services.AddScoped<IInsumoInterface, InsumoService>();
builder.Services.AddScoped<IImagenService, ImagenService>();
builder.Services.AddScoped<IUnidadInterface, UnidadService>();
builder.Services.AddScoped<IDetalleInsumoInterface, DetalleInsumoService>();
builder.Services.AddScoped<IAuditoriaInterface, AuditoriaService>();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
