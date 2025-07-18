using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Documents.Despedida;

[Table("despedidas")]
public class Despedida
{
    [Key]
    public int id_des { get; set; }
    public string despedida { get; set; }
}