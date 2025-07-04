using backend.Models.Areas;


namespace backend.Interfaces.Areas
{
    public interface IAreasService
    {
        IEnumerable<Area> GetAllAreas();
        Area? GetAreaById(int id);
        Area? CreateArea(Area area);
        Area? UpdateArea(Area area);
        bool DeleteArea(int id);
    }
}
