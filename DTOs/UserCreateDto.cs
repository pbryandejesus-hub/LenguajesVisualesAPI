using System.ComponentModel.DataAnnotations;

namespace LenguajesVisualesAPI.Dtos
{
    public class UserCreateDto
    {
        [Required] public string Nombre { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public string Rol { get; set; } = "user";
    }
}
