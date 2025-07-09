using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Recibidos.Internos;

[Table("interna_dir")]
public class InternaModel
{
    [Key]
    public int id_int { get; set; }
    public DateTime fecha_rec { get; set; }
    public string folio { get; set; }
    public string asunto { get; set; }
    public string procedencia { get; set; }
    public string destinatario { get; set; }
    public string ?no_memo { get; set; }
    public string ?no_of { get; set; }
    public string ?no_cir { get; set; }
    public DateTime fecha_doc { get; set; }
    public string hora { get; set; }
    public string archivo { get; set; }
    public byte[]? doc { get; set; }
}