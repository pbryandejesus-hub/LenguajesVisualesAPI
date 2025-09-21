using System.ComponentModel.DataAnnotations;

namespace LenguajesVisualesAPI.Dtos
{
    public class PedidoDto
    {
        [Required] public int UsuarioId { get; set; }
    }
}
