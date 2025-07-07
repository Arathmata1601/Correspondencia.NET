using backend.Interfaces.config;
using backend.Models.config;
using backend.Db;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.config
{
    public class ConfigService : IConfigService
    {
        private readonly AppDbContext _context;

        public ConfigService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Config> GetConfig()
        {
            return _context.Configuracion.ToList();
        }
/*
        public Configuracion? UpdateConfig(Configuracion config)
        {
            var configExistente = _context.Configuracion.FirstOrDefault(c => c.idconf == config.idconf);
            if (configExistente == null)
                return null;

            configExistente.ImgPrincipal = config.ImgPrincipal;
            configExistente.ImgSecun = config.ImgSecun;
            configExistente.ImgDoc = config.ImgDoc;
            configExistente.ImgFooter = config.ImgFooter;
            configExistente.ColorBotones = config.ColorBotones;
            configExistente.HoverBotones = config.HoverBotones;
            configExistente.ColorPagPrincipal = config.ColorPagPrincipal;

            _context.SaveChanges();
            return configExistente;
        }*/
    }
}