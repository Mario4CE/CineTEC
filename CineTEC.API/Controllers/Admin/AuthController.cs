using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineTec.API.Data;
using CineTec.API.DTOs;

namespace CineTec.API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly CineTecDbContext _context;

        public AuthController(CineTecDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Usuario) || string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                return BadRequest(new { mensaje = "Usuario y contraseña son requeridos" });
            }

            var admin = await _context.Administradores
                .FirstOrDefaultAsync(a => a.Usuario == dto.Usuario);

            if (admin == null)
            {
                return Unauthorized(new { mensaje = "Usuario no encontrado" });
            }

            if (admin.Contrasena != dto.Contrasena)
            {
                return Unauthorized(new { mensaje = "Contraseña incorrecta" });
            }

            return Ok(new
            {
                mensaje = "Login correcto",
                usuario = admin.Usuario,
                nombre = admin.Nombre,
                rol = admin.Rol
            });
        }
    }
}