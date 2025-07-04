using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace backend.Models.Documents;

[Table("documentos")]
public class Documento
{
    [Key]
    public int id_doc { get; set; }
    public int noMemo { get; set; }
    public string area_pro { get; set; }
    public DateTime fechaDoc { get; set; }
    public string asuntoDoc { get; set; }
    public string lugarDOc { get; set; }
    public string descripcion { get; set; }
    public string usuarioEmisor { get; set; }
    public string usuarioReceptor { get; set; }
    public string? estatusDoc { get; set; }
    public string area_rec { get; set; }
    public string introduccion { get; set; }
    public string despedida { get; set; }
    public string elaborado { get; set; }
    public string? atencion { get; set; }
    
    // 🔴 Esta propiedad no existe en la base de datos
    [NotMapped]
    public List<int> conCopiaResponsablesIds { get; set; } = new();

    [NotMapped]
    public string? otroNombre { get; set; }

    [NotMapped]
    public string? otroPuesto { get; set; }
}