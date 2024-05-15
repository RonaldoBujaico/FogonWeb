using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{


    [Table("categoria_insumo")]
    public class CategoriaInsumo
    {
        [Key]
        [Column("categoria_insumo_id")]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }


    }


}
