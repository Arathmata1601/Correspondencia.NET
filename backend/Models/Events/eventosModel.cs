namespace backend.Models.Events;

public class EventosModel
{
    public int id_evento { get; set; }
    public string nombre_evento { get; set; }
    public string descrip { get; set; }
    public string fecha_inicio { get; set; }
    public string fecha_fin { get; set; }
    public string hora_inicio { get; set; }
    public string hora_fin { get; set; }
    public string lugar { get; set; }
    public string area { get; set; }
    public string estado { get; set; } // Activo, Inactivo, Cancelado
}