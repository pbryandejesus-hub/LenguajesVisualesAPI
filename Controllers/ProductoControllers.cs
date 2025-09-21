using InventarioAPI.DTOs;
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
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _context.Productos.Include(p => p.Categoria).Select(p => new {
                p.Id,
                p.Nombre,
                p.Precio,
                Categoria = new { p.Categoria.Id, p.Categoria.Nombre }
            }).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var prod = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
            if (prod == null) return NotFound();
            return Ok(prod);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ProductoDto dto)
        {
            var cat = await _context.Categorias.FindAsync(dto.CategoriaId);
            if (cat == null) return BadRequest(new { mensaje = "Categoria no existe" });

            var prod = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                CategoriaId = dto.CategoriaId
            };

            _context.Productos.Add(prod);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = prod.Id }, prod);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDto dto)
        {
            var existing = await _context.Productos.FindAsync(id);
            if (existing == null) return NotFound();

            var cat = await _context.Categorias.FindAsync(dto.CategoriaId);
            if (cat == null) return BadRequest(new { mensaje = "Categoria no existe" });

            existing.Nombre = dto.Nombre;
            existing.Precio = dto.Precio;
            existing.CategoriaId = dto.CategoriaId;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _context.Productos.FindAsync(id);
            if (prod == null) return NotFound();
            _context.Productos.Remove(prod);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
