using BCrypt.Net;
using LenguajesVisualesAPI.Data;
using LenguajesVisualesAPI.Dtos;
using LenguajesVisualesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LenguajesVisualesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

            // Creamos el token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("rol", user.Rol)
            };

            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:ExpirationMinutes"])),
                signingCredentials: creds
            );

            return Ok(new
            {
                success = true,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in = double.Parse(_configuration["JWT:ExpirationMinutes"]) * 60,
                token_type = "Bearer"
            });
        }
    }
}
