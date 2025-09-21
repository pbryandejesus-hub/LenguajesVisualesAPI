﻿using System;
using System.Collections.Generic;

namespace LenguajesVisualesAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }  // ← AGREGAR ESTA LÍNEA
        public ICollection<DetallePedido> DetallesPedido { get; set; }
    }
}
