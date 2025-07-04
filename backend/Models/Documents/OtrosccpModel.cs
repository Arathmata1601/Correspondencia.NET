using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Documents.otros;

[Table("otrosccp")]
public class OtrosCcpModel
{
    [Key]
    public int id_otros { get; set; }
    public string nombre_otro { get; set; }
    public string puesto_otro { get; set; }
}