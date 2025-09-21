using BCrypt.Net;
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
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Usuarios.Select(u => new { u.Id, u.Nombre, u.Email, u.Rol }).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) return NotFound();
            return Ok(new { u.Id, u.Nombre, u.Email, u.Rol });
        }

        // Registro (no requiere JWT para permitir registrar)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDto dto)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Email == dto.Email))
                return BadRequest(new { mensaje = "El email ya está en uso" });

            var user = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = dto.Rol ?? "user"
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, new { user.Id, user.Nombre, user.Email, user.Rol });
        }
    }
}
