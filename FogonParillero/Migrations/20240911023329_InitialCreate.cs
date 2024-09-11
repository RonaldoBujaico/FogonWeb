using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FogonParillero.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoria_insumo",
                columns: table => new
                {
                    categoria_insumo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_insumo", x => x.categoria_insumo_id);
                });

            migrationBuilder.CreateTable(
                name: "categoria_producto",
                columns: table => new
                {
                    categoria_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    imagen_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_producto", x => x.categoria_id);
                });

            migrationBuilder.CreateTable(
                name: "Mesa",
                columns: table => new
                {
                    mesa_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    numero_mesa = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    usu_creador = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fech_creador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usu_modifica = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fech_modifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesa", x => x.mesa_id);
                });

            migrationBuilder.CreateTable(
                name: "unidad",
                columns: table => new
                {
                    unidad_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unidad", x => x.unidad_id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(500)", maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.usuario_id);
                });

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    producto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoria_producto_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.producto_id);
                    table.ForeignKey(
                        name: "FK_producto_categoria_producto_categoria_producto_id",
                        column: x => x.categoria_producto_id,
                        principalTable: "categoria_producto",
                        principalColumn: "categoria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesaId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                    table.ForeignKey(
                        name: "FK_Pedidos_Mesa_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesa",
                        principalColumn: "mesa_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insumo",
                columns: table => new
                {
                    insumo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoria_insumo_id = table.Column<int>(type: "int", nullable: false),
                    unidad_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insumo", x => x.insumo_id);
                    table.ForeignKey(
                        name: "FK_insumo_categoria_insumo_categoria_insumo_id",
                        column: x => x.categoria_insumo_id,
                        principalTable: "categoria_insumo",
                        principalColumn: "categoria_insumo_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insumo_unidad_unidad_id",
                        column: x => x.unidad_id,
                        principalTable: "unidad",
                        principalColumn: "unidad_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    auditoria_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_operacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre_tabla_afectada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_registro_afectado = table.Column<int>(type: "int", nullable: false),
                    detalles_cambio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_hora_operacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.auditoria_id);
                    table.ForeignKey(
                        name: "FK_Auditoria_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "usuario_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoDetalles",
                columns: table => new
                {
                    PedidoDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoDetalles", x => x.PedidoDetalleId);
                    table.ForeignKey(
                        name: "FK_PedidoDetalles_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoDetalles_producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "producto",
                        principalColumn: "producto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_insumo",
                columns: table => new
                {
                    detalle_insumo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    insumo_id = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_insumo", x => x.detalle_insumo_id);
                    table.ForeignKey(
                        name: "FK_detalle_insumo_insumo_insumo_id",
                        column: x => x.insumo_id,
                        principalTable: "insumo",
                        principalColumn: "insumo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_usuario_id",
                table: "Auditoria",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_insumo_insumo_id",
                table: "detalle_insumo",
                column: "insumo_id");

            migrationBuilder.CreateIndex(
                name: "IX_insumo_categoria_insumo_id",
                table: "insumo",
                column: "categoria_insumo_id");

            migrationBuilder.CreateIndex(
                name: "IX_insumo_unidad_id",
                table: "insumo",
                column: "unidad_id");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_PedidoId",
                table: "PedidoDetalles",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_ProductoId",
                table: "PedidoDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_MesaId",
                table: "Pedidos",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_producto_categoria_producto_id",
                table: "producto",
                column: "categoria_producto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "detalle_insumo");

            migrationBuilder.DropTable(
                name: "PedidoDetalles");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "insumo");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "categoria_insumo");

            migrationBuilder.DropTable(
                name: "unidad");

            migrationBuilder.DropTable(
                name: "Mesa");

            migrationBuilder.DropTable(
                name: "categoria_producto");
        }
    }
}
