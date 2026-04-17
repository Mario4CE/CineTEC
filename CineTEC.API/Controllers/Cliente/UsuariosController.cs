/*
 Entrada: DTOs de Usuario en requests POST/PUT
 Salida: UsuarioResponseDto en respuestas
 Restricciones: Validar datos requeridos, cédula única
 Descripción: Controlador que expone endpoints para la gestión de usuarios/clientes
 Modificado por: Mario
*/

using Microsoft.AspNetCore.Mvc;
using CineTec.API.DTOs;
using CineTec.API.Services;

namespace CineTec.API.Controllers.Cliente
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(UsuarioService usuarioService, ILogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Registra un nuevo usuario/cliente en el sistema
        /// </summary>
        /// <param name="dto">Datos del usuario a registrar</param>
        /// <returns>Usuario registrado</returns>
        /// <remarks>
        /// POST /api/usuarios/registrar
        /// {
        ///   "nombre": "Juan",
        ///   "apellido": "Pérez",
        ///   "cedula": "1234567890",
        ///   "telefono": "8765432109",
        ///   "fechaNacimiento": "1990-05-15"
        /// }
        /// </remarks>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioDto dto)
        {
            try
            {
                // Validar que los campos requeridos no estén vacíos
                if (string.IsNullOrWhiteSpace(dto.Nombre) ||
                    string.IsNullOrWhiteSpace(dto.Apellido) ||
                    string.IsNullOrWhiteSpace(dto.Cedula))
                {
                    return BadRequest(new
                    {
                        mensaje = "Los campos nombre, apellido y cédula son requeridos"
                    });
                }

                var usuarioRegistrado = await _usuarioService.RegistrarUsuario(dto);

                _logger.LogInformation($"Usuario registrado exitosamente: {usuarioRegistrado.Cedula}");

                return Created(nameof(ObtenerPorId), new
                {
                    mensaje = "Usuario registrado exitosamente",
                    usuario = usuarioRegistrado
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"Error al registrar usuario: {ex.Message}");
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al registrar usuario: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Verifica las credenciales de un usuario (login)
        /// </summary>
        /// <param name="dto">DTO con la cédula del usuario</param>
        /// <returns>Usuario si existe</returns>
        /// <remarks>
        /// POST /api/usuarios/login
        /// {
        ///   "cedula": "1234567890"
        /// }
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CedulaLoginDto dto)
        {
            try
            {
                string cedula = dto?.Cedula;

                if (string.IsNullOrWhiteSpace(cedula))
                {
                    return BadRequest(new { mensaje = "La cédula es requerida" });
                }

                var usuario = await _usuarioService.VerificarUsuario(cedula);

                if (usuario == null)
                {
                    _logger.LogWarning($"Intento de login fallido. Cédula no encontrada: {cedula}");
                    return Unauthorized(new { mensaje = "Cédula no encontrada en el sistema" });
                }

                _logger.LogInformation($"Login exitoso: {cedula}");

                return Ok(new
                {
                    mensaje = "Login exitoso",
                    usuario = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en login: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene un usuario por su cédula
        /// </summary>
        /// <param name="cedula">Cédula del usuario</param>
        /// <returns>Datos del usuario</returns>
        /// <remarks>
        /// GET /api/usuarios/cedula/1234567890
        /// </remarks>
        [HttpGet("cedula/{cedula}")]
        public async Task<IActionResult> ObtenerPorCedula(string cedula)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cedula))
                {
                    return BadRequest(new { mensaje = "La cédula es requerida" });
                }

                var usuario = await _usuarioService.ObtenerUsuarioPorCedula(cedula);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                return Ok(new
                {
                    mensaje = "Usuario encontrado",
                    usuario = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuario por cédula: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Datos del usuario</returns>
        /// <remarks>
        /// GET /api/usuarios/1
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorId(id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                return Ok(new
                {
                    mensaje = "Usuario encontrado",
                    usuario = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuario por ID: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios activos
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        /// <remarks>
        /// GET /api/usuarios
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerTodosLosUsuarios();

                return Ok(new
                {
                    mensaje = "Usuarios obtenidos exitosamente",
                    total = usuarios.Count,
                    usuarios = usuarios
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuarios: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Actualiza datos de un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="dto">Nuevos datos del usuario</param>
        /// <returns>Usuario actualizado</returns>
        /// <remarks>
        /// PUT /api/usuarios/1
        /// {
        ///   "nombre": "Juan",
        ///   "apellido": "García",
        ///   "cedula": "1234567890",
        ///   "telefono": "8765432100",
        ///   "fechaNacimiento": "1990-05-15"
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UsuarioDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(new { mensaje = "Los datos del usuario son requeridos" });
                }

                var usuarioActualizado = await _usuarioService.ActualizarUsuario(id, dto);

                _logger.LogInformation($"Usuario actualizado: {id}");

                return Ok(new
                {
                    mensaje = "Usuario actualizado exitosamente",
                    usuario = usuarioActualizado
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"Error al actualizar usuario: {ex.Message}");
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar usuario: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Eliminan un usuario (eliminación lógica - desactiva el usuario)
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Confirmación de eliminación</returns>
        /// <remarks>
        /// DELETE /api/usuarios/1
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _usuarioService.EliminarUsuario(id);

                _logger.LogInformation($"Usuario eliminado (desactivado): {id}");

                return Ok(new { mensaje = "Usuario eliminado exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"Error al eliminar usuario: {ex.Message}");
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al eliminar usuario: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }
    }
}
