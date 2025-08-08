using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Documents.ccp;

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
        public Documento Documento { get; set; }
    }