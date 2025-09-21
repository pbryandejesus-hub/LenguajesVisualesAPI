using System.Collections.Generic;

namespace LenguajesVisualesAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }  // ← AGREGAR ESTA LÍNEA
        public ICollection<Producto> Productos { get; set; }
    }
}
