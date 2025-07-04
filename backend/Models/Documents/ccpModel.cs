using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Documents.ccp;

[Table("concopia")]
public class CcpModel
{
    [Key]
    public int id_ccp { get; set; }
    public int id_docCC { get; set; }
    public int responsableDoc { get; set; }

}