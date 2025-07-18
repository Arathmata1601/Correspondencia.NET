using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Documents.Introduccion;

[Table ("introducciones")]
public class Introduccion
{
    [Key]
    public int idIntro { get; set; }
    public string intro { get; set; }
}