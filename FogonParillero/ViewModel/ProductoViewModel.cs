﻿using FogonParillero.Models;

namespace FogonParillero.ViewModel
{
    public class ProductoViewModel
    {
        public IEnumerable<Producto> Productos { get; set; }
        public IEnumerable<Insumo> Insumos { get; set; }
        public IEnumerable<CategoriaProducto> Categorias { get; set; }
    }
}
