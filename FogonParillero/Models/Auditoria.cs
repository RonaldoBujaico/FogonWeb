using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FogonParillero.Models
{
    public class Auditoria
    {
        [Key]
        [Column("auditoria_id")]
        public int Id { get; set; }
        [Column("tipo_operacion")]
        public string TipoOperacion { get; set; }
        [Column("nombre_tabla_afectada")]
        public string NombreTablaAfectada { get; set; }
        [Column("id_registro_afectado")]
        public int IdRegistroAfectado { get; set; }
        [Column("detalles_cambio")]
        public string DetallesCambio { get; set; }
        [Column("fecha_hora_operacion")]
        public DateTime FechaHoraOperacion { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
