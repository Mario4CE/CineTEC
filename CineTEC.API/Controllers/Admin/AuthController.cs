/*
 Entrada: POST /api/auth/login
 Salida: 200 OK
 {
   "mensaje": "Login correcto",
   "usuario": "admin1",
   "nombre": "Admin Uno",
   "rol": "Administrador"
 }
Descrpccion: Este endpoint permite a los administradores iniciar sesión en el sistema. Recibe un objeto JSON con las propiedades "usuario" y "contrasena".
            Si las credenciales son correctas, devuelve un mensaje de éxito junto con la información del usuario.
            Si las credenciales son incorrectas, devuelve un mensaje de error indicando que el usuario o la contraseña son incorrectos.
Modificado por: Mario
*/
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
            var admin = await _context.Administradores
                .FirstOrDefaultAsync(a => a.Usuario == dto.Usuario && a.Contrasena == dto.Contrasena);

            if (admin == null)
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
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