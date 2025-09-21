using System.Collections.Generic;

namespace LenguajesVisualesAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        // Guardamos hash de la contraseña
        public string PasswordHash { get; set; }
        public string Rol { get; set; }

        public List<Pedido> Pedidos { get; set; }
    }
}
