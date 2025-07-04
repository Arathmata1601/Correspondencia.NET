namespace backend.Models.Llamadas;

public class LlamadasSalModel
{
    public int id_llamada { get; set; }
    public DateTime fecha_llamada { get; set; }
    public string area_ { get; set; }
    public string remitente { get; set; }
    public string hora_llamada { get; set; }
    public string llam { get; set; }
    public string seguimiento { get; set; }
    public string asunto_llamada { get; set; }
    public string usuarioSal { get; set; }
}