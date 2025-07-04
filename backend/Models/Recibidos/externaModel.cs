namespace backend.Models.Recibidos;

public class ExternaModel
{
    public int id_externa { get; set; }
    public DateTime fecha_rec { get; set; }
    public string folio { get; set; }
    public string asunto { get; set; }
    public string nombre { get; set; }
    public string cargo { get; set; }
    public string dependencia { get; set; }
    public string tipo { get; set; }
    public DateTime fecha_doc { get; set; }
    public string hora { get; set; }
    public string medio { get; set; }
    public DateTime fecha_ven { get; set; }
    public string turnado { get; set; }
    public string indicacion { get; set; }
    public string asignado { get; set; }
    public byte[] ?doc { get; set; }
}