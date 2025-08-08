using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models.Documents
{
    [Table("documentos")]
    public class Documento
    {
        [Key]
        public int id_doc { get; set; }
        public int noMemo { get; set; }
        public string area_pro { get; set; }
        public DateTime fechaDoc { get; set; }
        public string asuntoDoc { get; set; }
        public string lugarDOc { get; set; }
        public string descripcion { get; set; }
        public string usuarioEmisor { get; set; }
        public string usuarioReceptor { get; set; }
        public string? estatusDoc { get; set; }
        public string area_rec { get; set; }
        public string introduccion { get; set; }
        public string despedida { get; set; }
        public string elaborado { get; set; }
        public string? atencion { get; set; }

        // Relación con CCP
        public ICollection<CcpModel> ConCopias { get; set; } = new List<CcpModel>();

        // No mapeados
        [NotMapped]
        public List<int> conCopiaResponsablesIds { get; set; } = new();

        [NotMapped]
        public string? otroNombre { get; set; }

        [NotMapped]
        public string? otroPuesto { get; set; }
    }

    [Table("concopia")]
    public class CcpModel
    {
        [Key]
        public int id_ccp { get; set; }

        // FK hacia Documento
        [ForeignKey(nameof(Documento))]
        public int id_docCC { get; set; }

        public int responsableDoc { get; set; }

        // Navegación inversa
        [JsonIgnore]
        public Documento Documento { get; set; }
    }
}
