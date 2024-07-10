using System.ComponentModel.DataAnnotations.Schema;

namespace FogonParillero.Models
{
    [Table("insumo")]
    public class Insumo
    {
        [Column("insumo_id")]
        public int InsumoId { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("descripcion")]
        public string? Descripcion { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("imagen")]
        public string? Imagen { get; set; }

        [Column("categoria_insumo_id")]
        public int CategoriaInsumoId { get; set; }
        public CategoriaInsumo Categoria { get; set; }

        [Column("unidad_id")]
        public int UnidadId { get; set; }

        public Unidad Unidad { get; set; }




    }
}
