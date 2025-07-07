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

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar servicios
// Cambiar de AddControllersWithViews() a AddControllers() para API
builder.Services.AddControllers();

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

var app = builder.Build();

// 2. Configurar middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

// ¡IMPORTANTE! Primero la autenticación, luego la autorización
app.UseAuthentication();
app.UseAuthorization();

// Archivos estáticos (solo si los necesitas)
app.UseStaticFiles();

// CAMBIO PRINCIPAL: Usar MapControllers() en lugar de MapControllerRoute()
app.MapControllers();

// Si también tienes controladores MVC, puedes mantener ambos:
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();