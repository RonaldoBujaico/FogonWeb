using System;
using System.Linq;
using FogonParillero.Data; // Ajusta el namespace según tu proyecto
using FogonParillero.Models;
using FogonParillero.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FogonParillero.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationContext _context;

        public PedidoController(ApplicationContext context)
        {
            _context = context;
        }

        // Acción GET para mostrar la vista de detalle de pedido
        public IActionResult Index()
        {
            // Aquí podrías implementar la lógica para cargar datos necesarios para la vista de detalle de pedido
            return View();
        }

        // Acción POST para guardar un pedido
        [HttpPost]
        public IActionResult GuardarPedido([FromBody] GuardarPedidoViewModel pedidoVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Crear un nuevo pedido en la base de datos
                    var nuevoPedido = new Pedido
                    {
                        MesaId = pedidoVM.MesaId,
                        FechaPedido = DateTime.Now,
                        Estado = "Pendiente", // Puedes establecer un estado inicial
                        Total = pedidoVM.Productos.Sum(p => p.Cantidad * p.Precio) // Calcular el total del pedido
                    };

                    _context.Pedido.Add(nuevoPedido);

                    // Actualizar el estado de la mesa a "Ocupado"
                    var mesa = _context.Mesa.FirstOrDefault(m => m.MesaId == pedidoVM.MesaId);
                    if (mesa != null)
                    {
                        mesa.Estado = "Ocupado";
                        _context.Mesa.Update(mesa);
                    }

                    _context.SaveChanges();

                    // Guardar los detalles del pedido (productos seleccionados)
                    foreach (var producto in pedidoVM.Productos)
                    {
                        var detallePedido = new PedidoDetalle
                        {
                            PedidoId = nuevoPedido.PedidoId,
                            ProductoId = producto.ProductoId,
                            Cantidad = producto.Cantidad,
                            Precio = producto.Precio
                        };

                        _context.PedidoDetalle.Add(detallePedido);
                    }

                    _context.SaveChanges(); // Guardar los cambios en la base de datos

                    // Respuesta exitosa
                    return Json(new { success = true, message = "Pedido guardado exitosamente." });
                }
                catch (Exception ex)
                {
                    // Manejar errores de forma adecuada
                    return Json(new { success = false, message = $"Error al guardar el pedido: {ex.Message}" });
                }
            }
            else
            {
                // Si hay errores de validación, devolver un mensaje de error
                var errorMessage = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Json(new { success = false, message = $"Error al guardar el pedido. Datos inválidos: {errorMessage}" });
            }
        }

        // Acción POST para actualizar el estado de un pedido
        [HttpPost]
        public IActionResult ActualizarEstadoPedido(int pedidoId, string nuevoEstado)
        {
            try
            {
                var pedido = _context.Pedido.FirstOrDefault(p => p.PedidoId == pedidoId);
                if (pedido != null)
                {
                    pedido.Estado = nuevoEstado;
                    _context.Pedido.Update(pedido);

                    // Actualizar el estado de la mesa en función del nuevo estado del pedido
                    var mesa = _context.Mesa.FirstOrDefault(m => m.MesaId == pedido.MesaId);
                    if (mesa != null)
                    {
                        mesa.Estado = nuevoEstado == "Cancelado" ? "Vacio" : "Ocupado";
                        _context.Mesa.Update(mesa);
                    }

                    _context.SaveChanges();
                    return Json(new { success = true, message = "Estado del pedido actualizado exitosamente." });
                }
                else
                {
                    return Json(new { success = false, message = "Pedido no encontrado." });
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de forma adecuada
                return Json(new { success = false, message = $"Error al actualizar el estado del pedido: {ex.Message}" });
            }
        }
    }
}
