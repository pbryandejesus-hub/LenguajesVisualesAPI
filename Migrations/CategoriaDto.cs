using System.ComponentModel.DataAnnotations;

namespace LenguajesVisualesAPI.Dtos
{
    public class CategoriaDto
    {
        [Required] public string Nombre { get; set; }
    }
}
