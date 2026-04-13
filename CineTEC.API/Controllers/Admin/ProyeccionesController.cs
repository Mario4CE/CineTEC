/* 
Entrada: Solicitudes HTTP (GET, POST, DELETE)
Salida: Respuestas HTTP con datos o mensajes en formato JSON
Restricciones: La película y la sala deben existir en la base de datos
Descripción: Controlador que gestiona las proyecciones del sistema CineTec,
incluyendo consulta, creación y eliminación de funciones de películas
Modificado por: Mario
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineTec.API.Data;
using CineTec.API.DTOs;
using CineTec.API.Models;

namespace CineTec.API.Controllers.Admin
{
    [ApiController] // Indica que es un controlador de API
    [Route("api/[controller]")] // Ruta base: api/proyecciones
    public class ProyeccionesController : ControllerBase
    {
        private readonly CineTecDbContext _context; // Contexto de base de datos

        // Constructor con inyección de dependencias
        public ProyeccionesController(CineTecDbContext context)
        {
            _context = context;
        }

        // ============================
        // GET: api/proyecciones
        // ============================
        // Obtiene todas las proyecciones con sus relaciones
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proyecciones = await _context.Proyecciones
                .Include(p => p.Pelicula) // Incluye la película
                .Include(p => p.Sala) // Incluye la sala
                .ThenInclude(s => s!.Sucursal) // Incluye la sucursal de la sala
                .ToListAsync();

            return Ok(proyecciones); // Retorna lista completa
        }

        // ============================
        // POST: api/proyecciones
        // ============================
        // Crea una nueva proyección
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProyeccionDto dto)
        {
            // Verifica que la película exista
            var peliculaExiste = await _context.Peliculas.AnyAsync(p => p.Id == dto.PeliculaId);

            // Verifica que la sala exista
            var salaExiste = await _context.Salas.AnyAsync(s => s.Id == dto.SalaId);

            // Validación de datos
            if (!peliculaExiste || !salaExiste)
                return BadRequest(new { mensaje = "Película o sala no válida" });

            // Se crea la nueva proyección
            var proyeccion = new Proyeccion
            {
                PeliculaId = dto.PeliculaId,
                SalaId = dto.SalaId,
                Fecha = dto.Fecha
            };

            _context.Proyecciones.Add(proyeccion); // Se agrega a la BD
            await _context.SaveChangesAsync(); // Guarda cambios

            return Ok(new { mensaje = "Proyección creada correctamente", proyeccion });
        }

        // ============================
        // DELETE: api/proyecciones/{id}
        // ============================
        // Elimina una proyección
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var proyeccion = await _context.Proyecciones.FindAsync(id); // Busca la proyección

            if (proyeccion == null)
                return NotFound(new { mensaje = "Proyección no encontrada" });

            _context.Proyecciones.Remove(proyeccion); // Elimina de la BD
            await _context.SaveChangesAsync(); // Guarda cambios

            return Ok(new { mensaje = "Proyección eliminada correctamente" });
        }
    }
}