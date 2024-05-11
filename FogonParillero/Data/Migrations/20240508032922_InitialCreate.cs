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
                name: "usuario",
                columns: table => new
                {
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    imagen_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    categoria_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.producto_id);
                    table.ForeignKey(
                        name: "FK_producto_categoria_producto_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categoria_producto",
                        principalColumn: "categoria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_producto_categoria_id",
                table: "producto",
                column: "categoria_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "categoria_producto");
        }
    }
}
