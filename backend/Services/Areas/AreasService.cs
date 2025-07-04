using backend.Interfaces.Areas;
using backend.Models.Areas;
using backend.Db;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace backend.Services.Areas
{
    public class AreasService : IAreasService
    {
        private readonly AppDbContext _context;

        public AreasService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Area> GetAllAreas()
        {
            return _context.Areas.ToList();
        }

        public Area? GetAreaById(int id)
        {
            return _context.Areas.FirstOrDefault(a => a.id_area == id);
        }

        public Area? CreateArea(Area area)
        {
            _context.Areas.Add(area);
            _context.SaveChanges();
            return area;
        }

        public Area? UpdateArea(Area area)
        {
            var existingArea = _context.Areas.FirstOrDefault(a => a.id_area == area.id_area);
            if (existingArea == null)
                return null;

            existingArea.nombre_area = area.nombre_area;
            existingArea.responsable = area.responsable;
            existingArea.in_Resp = area.in_Resp;
            existingArea.Puesto = area.Puesto; 
            existingArea.Iniciales = area.Iniciales;

            _context.SaveChanges();
            return existingArea;
        }

        public bool DeleteArea(int id)
        {
            var area = _context.Areas.FirstOrDefault(a => a.id_area == id);
            if (area == null)
                return false;

            _context.Areas.Remove(area);
            _context.SaveChanges();
            return true;
        }
    }
}
