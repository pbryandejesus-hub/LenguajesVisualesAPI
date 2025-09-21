using LenguajesVisualesAPI.Data;
using LenguajesVisualesAPI.Dtos;
using LenguajesVisualesAPI.Dtos;
using LenguajesVisualesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenguajesVisualesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Categorias.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CategoriaDto dto)
        {
            var cat = new Categoria { Nombre = dto.Nombre };
            _context.Categorias.Add(cat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] CategoriaDto dto)
        {
            var existing = await _context.Categorias.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return NotFound();
            _context.Categorias.Remove(cat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
