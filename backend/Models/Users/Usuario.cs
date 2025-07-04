using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;    
namespace backend.Models.Users;

[Table("usuarios")]
public class Usuario

{
    [Key]
    public int id_us { get; set; }
    public string Nombre_us { get; set; }
    public string Apellidos { get; set; }
    public string usuario { get; set; }
    public string area { get; set; }
    public string rol { get; set; }

    public string contraseña { get; set; }
}