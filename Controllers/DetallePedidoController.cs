using LenguajesVisualesAPI.Data;
using LenguajesVisualesAPI.Dtos;
using LenguajesVisualesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenguajesVisualesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DetallePedidoController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _context.DetallesPedido.Include(d => d.Pedido).Include(d => d.Producto).ToListAsync());

        [HttpGet("{pedidoId}/{productoId}")]
        public async Task<IActionResult> Get(int pedidoId, int productoId)
        {
            var detalle = await _context.DetallesPedido.Include(d => d.Pedido).Include(d => d.Producto)
                .FirstOrDefaultAsync(d => d.PedidoId == pedidoId && d.ProductoId == productoId);
            if (detalle == null) return NotFound();
            return Ok(detalle);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] DetallePedidoDto dto)
        {
            var pedido = await _context.Pedidos.FindAsync(dto.PedidoId);
            if (pedido == null) return BadRequest(new { mensaje = "Pedido no existe" });

            var producto = await _context.Productos.FindAsync(dto.ProductoId);
            if (producto == null) return BadRequest(new { mensaje = "Producto no existe" });

            var existing = await _context.DetallesPedido.FindAsync(dto.PedidoId, dto.ProductoId);
            if (existing != null) return BadRequest(new { mensaje = "Detalle ya existe, usa PUT para actualizar cantidad" });

            var detalle = new DetallePedido
            {
                PedidoId = dto.PedidoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad
            };

            _context.DetallesPedido.Add(detalle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { pedidoId = detalle.PedidoId, productoId = detalle.ProductoId }, detalle);
        }

        [HttpPut("{pedidoId}/{productoId}")]
        [Authorize]
        public async Task<IActionResult> Put(int pedidoId, int productoId, [FromBody] DetallePedidoDto dto)
        {
            var existing = await _context.DetallesPedido.FindAsync(pedidoId, productoId);
            if (existing == null) return NotFound();

            existing.Cantidad = dto.Cantidad;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{pedidoId}/{productoId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int pedidoId, int productoId)
        {
            var detalle = await _context.DetallesPedido.FindAsync(pedidoId, productoId);
            if (detalle == null) return NotFound();
            _context.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
