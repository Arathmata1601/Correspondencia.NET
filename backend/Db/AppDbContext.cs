using Microsoft.EntityFrameworkCore;
using backend.Models.Auth;
using backend.Models.Users;
using backend.Models.Areas;
using backend.Models.config;
using backend.Models.Documents;
using backend.Models.Documents.ccp;
using backend.Models.Documents.otros;
using backend.Models.Events;

namespace backend.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Config> Configuracion { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Eventos> Eventos { get; set; } 
        public DbSet<CcpModel> ConCopia { get; set; }        
        public DbSet<OtrosCcpModel> OtrosCcp { get; set; }   
    }
}
