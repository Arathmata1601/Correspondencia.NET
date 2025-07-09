using System;
using System.ComponentModel.DataAnnotations;    
using System.ComponentModel.DataAnnotations.Schema;
using backend.Db;

namespace backend.Models.Llamadas.Salientes;

[Table("llamadassalientes")]
public class LlamadasSal
{
    [Key]
    public int id_llamada { get; set; }
    public DateTime fecha_llamada { get; set; }
    public string area_ { get; set; }
    public string remitente { get; set; }
    public string hora_llamada { get; set; }
    public string llam { get; set; }
    public string seguimiento { get; set; }
    public string asunto_llamada { get; set; }
    public string usuarioSal { get; set; }
}