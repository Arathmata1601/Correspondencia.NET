

public class DocumentoCreateDto
{
    public int noMemo { get; set; }
    public string area_rec { get; set; }
    public DateTime fechaDoc { get; set; }
    public string asuntoDoc { get; set; }
    public string descripcion { get; set; }
    public string despedida { get; set; }
    public string introduccion { get; set; }
    public string elaborado { get; set; }
    public string? atencion { get; set; }

    public List<int> conCopiaResponsablesIds { get; set; } = new(); // IDs de responsables (áreas o usuarios)
    public string? nombre_otro { get; set; }
    public string? puesto_otro { get; set; }
}
