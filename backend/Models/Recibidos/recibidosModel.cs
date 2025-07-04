namespace backend.Models.Recibidos;

public class RecibidosModel
{
    public int id_Rec { get; set; }
    public int numeroInt { get; set; }
    public string tipo { get; set; }
    public string noMemo { get; set; }
    public string Area_pro { get; set; }
    public DateTime Fecha { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }
    public string estatus { get; set; }
    public string areaRec { get; set; }
    public string turnado { get; set; }
    public byte[]? doc { get; set; }
}