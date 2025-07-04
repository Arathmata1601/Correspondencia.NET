namespace backend.Models.Llamadas;

public class LlamadasEntModel
{
    public int id_llamadaEn { get; set; }
    public DateTime fechaEn { get; set; }
    public string nombre_areaEn { get; set; }
    public string responsableEn { get; set; }
    public string hora_llamadaEn { get; set; }
    public string llamEn { get; set; }
    public string seguimientoEn { get; set; }
    
    public string asunto_llamadaEn { get; set; }
    public string Usuario { get; set; }
}