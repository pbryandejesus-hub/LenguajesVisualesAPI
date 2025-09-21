using System.ComponentModel.DataAnnotations;

namespace LenguajesVisualesAPI.Dtos
{
    public class DetallePedidoDto
    {
        [Required] public int PedidoId { get; set; }
        [Required] public int ProductoId { get; set; }
        [Required] public int Cantidad { get; set; }
    }
}
