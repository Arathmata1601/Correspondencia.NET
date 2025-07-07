using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace backend.Models.Events;

[Table("eventos")]
public class Eventos
{
    [Key]
    public int id_evento { get; set; }
    public string nombre_evento { get; set; }
    public string descrip { get; set; }
    public DateTime fecha_inicial { get; set; }
    public DateTime Fecha_final { get; set; }
    [Column ("hora_inicio")]
    public TimeSpan hora_inicio { get; set; }
    [Column ("hora_fin")]
    public TimeSpan hora_fin { get; set; }
    public string lugar { get; set; }
    public string area { get; set; }
    //public string? estado { get; set; } // Activo, Realizado, Cancelado
}