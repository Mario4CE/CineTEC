/*
 Entrada: Solicitudes HTTP GET
 Salida: Lista de sucursales disponibles
 Restricciones: Solo lectura, sin autenticación
 Descripción: Controlador público para que los clientes puedan ver las sucursales disponibles
 Modificado por: Mario
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineTec.API.Data;

namespace CineTec.API.Controllers.Cliente
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalesController : ControllerBase
    {
        private readonly CineTecDbContext _context;
        private readonly ILogger<SucursalesController> _logger;

        public SucursalesController(CineTecDbContext context, ILogger<SucursalesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las sucursales disponibles
        /// </summary>
        /// <returns>Lista de sucursales</returns>
        /// <remarks>
        /// GET /api/sucursales
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sucursales = await _context.Sucursales.ToListAsync();

                _logger.LogInformation($"Se obtuvieron {sucursales.Count} sucursales");

                return Ok(new
                {
                    mensaje = "Sucursales obtenidas correctamente",
                    sucursales = sucursales
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener sucursales: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene una sucursal por su ID
        /// </summary>
        /// <param name="id">ID de la sucursal</param>
        /// <returns>Datos de la sucursal</returns>
        /// <remarks>
        /// GET /api/sucursales/1
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.Id == id);

                if (sucursal == null)
                {
                    return NotFound(new { mensaje = "Sucursal no encontrada" });
                }

                return Ok(new
                {
                    mensaje = "Sucursal obtenida correctamente",
                    sucursal = sucursal
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener sucursal {id}: {ex.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }
    }
}
