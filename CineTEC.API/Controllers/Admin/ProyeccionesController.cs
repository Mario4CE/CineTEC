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
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ProyeccionesController : ControllerBase
    {
        private readonly CineTecDbContext _context;

        public ProyeccionesController(CineTecDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proyecciones = await _context.Proyecciones
                .Include(p => p.Pelicula)
                .Include(p => p.Sala)
                .ThenInclude(s => s!.Sucursal)
                .ToListAsync();

            return Ok(proyecciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proyeccion = await _context.Proyecciones
                .Include(p => p.Pelicula)
                .Include(p => p.Sala)
                .ThenInclude(s => s!.Sucursal)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proyeccion == null)
                return NotFound(new { mensaje = "Proyección no encontrada" });

            return Ok(proyeccion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProyeccionDto dto)
        {
            var peliculaExiste = await _context.Peliculas.AnyAsync(p => p.Id == dto.PeliculaId);
            var salaExiste = await _context.Salas.AnyAsync(s => s.Id == dto.SalaId);

            if (!peliculaExiste || !salaExiste)
                return BadRequest(new { mensaje = "Película o sala no válida" });

            var proyeccion = new Proyeccion
            {
                PeliculaId = dto.PeliculaId,
                SalaId = dto.SalaId,
                Fecha = dto.Fecha
            };

            _context.Proyecciones.Add(proyeccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = proyeccion.Id }, proyeccion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var proyeccion = await _context.Proyecciones.FindAsync(id);

            if (proyeccion == null)
                return NotFound(new { mensaje = "Proyección no encontrada" });

            _context.Proyecciones.Remove(proyeccion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proyección eliminada correctamente" });
        }
    }
}