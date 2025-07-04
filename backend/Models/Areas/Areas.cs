using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Areas;

[Table("areas")]
public class Area
{
    [Key]
    public int id_area { get; set; }
    public string nombre_area { get; set; }
    public string responsable { get; set; }
    public string in_Resp { get; set; }
    public string Puesto { get; set; }
    public string Iniciales { get; set; }
}