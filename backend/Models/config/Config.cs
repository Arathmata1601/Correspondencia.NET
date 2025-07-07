using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.config;

[Table("configuracion")]
public class Config
{
    [Key]
    public int idconf { get; set; }
    public byte[] ImgPrincipal { get; set; }
    public byte[] ImgSecun { get; set; }
    public byte[] ImgDoc { get; set; }
    public byte[] ImgFooter { get; set; }
    public string ColorBotones { get; set; }
    public string HoverBotones { get; set; }
    public string ColorPagPrincipal { get; set; }
    
}