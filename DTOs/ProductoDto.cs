namespace InventarioAPI.DTOs
{
    public class ProductoDto
    {
        public string Nombre { get; set; } = string.Empty;   // Nombre del producto
        public decimal Precio { get; set; }                  // Precio del producto
        public int CategoriaId { get; set; }                 // Relación con la categoría
    }
}
