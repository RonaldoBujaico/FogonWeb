using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FogonParillero.Data;
using FogonParillero.Models;

namespace FogonParillero.Controllers
{
    public class PedidoDetalleController : Controller
    {
        private readonly ApplicationContext _context;

        public PedidoDetalleController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pedidoId)
        {
            try
            {
                var detallesPedido = _context.PedidoDetalle
                    .Include(pd => pd.Producto)
                    .Where(pd => pd.PedidoId == pedidoId)
                    .ToList();

                return View(detallesPedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener detalles del pedido:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return StatusCode(500, "Error interno al obtener detalles del pedido");
            }
        }
    }
}
