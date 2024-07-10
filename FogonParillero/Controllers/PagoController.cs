using System;
using System.Linq;
using FogonParillero.Data;
using FogonParillero.Models;
using FogonParillero.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FogonParillero.Controllers
{
    public class PagoController : Controller
    {
        private readonly ApplicationContext _context;

        public PagoController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index(string mesaId)
        {
            try
            {
                // Obtener los pedidos pendientes para una mesa específica
                var pedidosPendientes = _context.Pedido
                    .Include(p => p.Mesa)
                    .Where(p => p.MesaId == mesaId && p.Estado == "Pendiente")
                    .ToList();

                // Preparar el ViewModel con los pedidos pendientes
                var viewModel = new ProductoViewModel
                {
                    Pedidos = pedidosPendientes
                };

                // Establecer el título de la vista
                ViewData["Title"] = "Ventas";

                // Asignar el nombre de la mesa seleccionada a ViewData
                ViewData["Detalle De Mesa"] = mesaId;

                // Devolver la vista con el ViewModel
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Manejar errores y loggear el error para poder investigar más a fondo
                Console.WriteLine("Error al cargar pedidos pendientes: " + ex.Message);
                return StatusCode(500, "Error interno al cargar pedidos pendientes");
            }
        }




        [HttpPost]
        public IActionResult Pagar(int pedidoId)
        {
            try
            {
                // Buscar el pedido por su ID
                var pedido = _context.Pedido.FirstOrDefault(p => p.PedidoId == pedidoId);

                if (pedido == null)
                {
                    return NotFound(); // Manejar caso donde no se encuentra el pedido
                }

                // Cambiar el estado del pedido a "Cancelado"
                pedido.Estado = "Cancelado";
                _context.SaveChanges(); // Guardar cambios en la base de datos

                // Actualizar el estado de la mesa a "Vacio"
                var mesa = _context.Mesa.FirstOrDefault(m => m.MesaId == pedido.MesaId);
                if (mesa != null)
                {
                    mesa.Estado = "Vacio";
                    _context.Mesa.Update(mesa);
                    _context.SaveChanges(); // Guardar cambios en la base de datos de la mesa
                }

                // Redirigir de regreso a la vista de ventas
                return RedirectToAction("Index", new { mesaId = pedido.MesaId });
            }
            catch (Exception ex)
            {
                // Manejar errores y loggear el error para poder investigar más a fondo
                Console.WriteLine("Error al procesar el pago: " + ex.Message);
                return StatusCode(500, "Error interno al procesar el pago");
            }
        }

        

    }
}
