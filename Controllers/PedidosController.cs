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
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PedidosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _context.Pedidos.Include(p => p.Usuario).Include(p => p.DetallesPedido).ThenInclude(d => d.Producto).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.Usuario).Include(p => p.DetallesPedido).ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PedidoDto dto)
        {
            var user = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (user == null) return BadRequest(new { mensaje = "Usuario no existe" });

            var pedido = new Pedido
            {
                UsuarioId = dto.UsuarioId,
                Fecha = DateTime.UtcNow
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] PedidoDto dto)
        {
            var existing = await _context.Pedidos.FindAsync(id);
            if (existing == null) return NotFound();

            var user = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (user == null) return BadRequest(new { mensaje = "Usuario no existe" });

            existing.UsuarioId = dto.UsuarioId;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
