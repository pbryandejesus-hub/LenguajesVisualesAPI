using System;
using System.Collections.Generic;

namespace LenguajesVisualesAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;   // evita warning
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }              // <-- asegurate que existe y es decimal
        public ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
    }
}
