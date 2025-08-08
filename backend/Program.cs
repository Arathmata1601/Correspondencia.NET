using backend.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using backend.Interfaces.Usuario;
using backend.Services.Auth;
using backend.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using backend.Interfaces.Users;
using backend.Services.Users;
using backend.Models.Users;
using backend.Interfaces.Areas;
using backend.Services.Areas;
using backend.Interfaces.config;
using backend.Services.config;
using backend.Models.config;
using backend.Interfaces.Documents;
using backend.Services.Documents;
using backend.Models.Documents;
using backend.Interfaces.Events;
using backend.Services.Events;
using backend.Models.Llamadas.Entrantes;
using backend.Interfaces.Llamadas;
using backend.Services.Llamadas.Entrantes;
using backend.Models.Llamadas.Salientes;
using backend.Interfaces.Llamadas;
using backend.Services.Llamadas.Salientes;
using backend.Interfaces.Recibidos;
using backend.Services.Recibidos;
using backend.Models.Recibidos;
using backend.Models.Recibidos.Externos;
using backend.Interfaces.Recibidos.Externos;
using backend.Services.Recibidos.Externos;
using backend.Models.Recibidos.Internos;
using backend.Interfaces.Recibidos.Internos;
using backend.Services.Recibidos.Internos;
using backend.Controllers;
using backend.Models.Documents.Despedida;
using backend.Models.Documents.Introduccion;
using backend.Interfaces.Complementos;
using backend.Services.Documents.Complementos;
using DinkToPdf;
using DinkToPdf.Contracts;



var builder = WebApplication.CreateBuilder(args);

// 1. Configurar servicios
// Cambiar de AddControllersWithViews() a AddControllers() para API
//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


// Cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IAreasService, AreasService>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IEntrantesServices, EntrantesService>();
builder.Services.AddScoped<ISalientesServices, SalientesService>();
builder.Services.AddScoped<IRecibidosService, RecibidosService>();
builder.Services.AddScoped<IExternaService, ExternaService>();
builder.Services.AddScoped<IInternaService, InternaService>();
builder.Services.AddScoped<IComplementosService, complementosService>();

var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Libraries/wkhtmltox/libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

var corsPolicy = "_allowFrontend";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000","http://localhost:5200")
        //policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agrega CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 2. Configurar middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


//app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowFrontend");

// ¡IMPORTANTE! Primero la autenticación, luego la autorización
app.UseAuthentication();
app.UseAuthorization();

// Archivos estáticos (solo si los necesitas)
app.UseStaticFiles();

// CAMBIO PRINCIPAL: Usar MapControllers() en lugar de MapControllerRoute()
app.MapControllers();

// Si también tienes controladores MVC, puedes mantener ambos:
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();