using Microsoft.EntityFrameworkCore;
using backend.Models.Auth;
using backend.Models.Users;
using backend.Models.Areas;
using backend.Models.config;
using backend.Models.Documents;
using backend.Models.Documents.ccp;
using backend.Models.Documents.otros;
using backend.Models.Events;
using backend.Models.Llamadas.Entrantes;
using backend.Models.Llamadas.Salientes;
using backend.Models.Recibidos;
using backend.Models.Recibidos.Externos;
using backend.Models.Recibidos.Internos;

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
        public DbSet<LlamadasEnt> LlamadasEntrantes { get; set; } 
        public DbSet<LlamadasSal> LlamadasSalientes { get; set; }
        public DbSet<DocRecibidos> Recibidos { get; set; }
        public DbSet<Externa> Externa { get; set; }
        public DbSet<InternaModel> Interna { get; set; } // Uncomment
    }
}
