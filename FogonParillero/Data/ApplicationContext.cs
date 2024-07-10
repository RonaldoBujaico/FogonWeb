using FogonParillero.Models;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CategoriaInsumo> CategoriasInsumo { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
<<<<<<< HEAD
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<DetalleInsumo> DetalleInsumos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalle { get; set; }
=======
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<DetalleInsumo> DetalleInsumos { get; set; }
>>>>>>> 2045db2c0ccac8e406a72b96d93ff057c405aa53
    }
}
