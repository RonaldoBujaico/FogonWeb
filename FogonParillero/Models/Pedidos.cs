using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FogonParillero.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PedidoId")]
        public int PedidoId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("MesaId")]
        public string MesaId { get; set; }

        [Required]
        [Column("FechaPedido")]
        public DateTime FechaPedido { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Estado")]
        public string Estado { get; set; }

        [Required]
        [Column("Total")]
        public decimal Total { get; set; }

        [ForeignKey("MesaId")]
        public Mesa Mesa { get; set; }
    }
}
