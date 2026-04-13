/* 
Entrada: Solicitudes HTTP (GET, POST, PUT, DELETE)
Salida: Respuestas HTTP con datos o mensajes en formato JSON
Restricciones: La base de datos debe estar disponible y la sucursal asociada debe existir
Descripción: Controlador que gestiona las salas del sistema CineTec,
incluyendo operaciones CRUD y validación de relaciones con sucursales
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
    [Route("api/[controller]")] // Ruta base: api/salas
    public class SalasController : ControllerBase
    {
        private readonly CineTecDbContext _context; // Contexto de base de datos

        // Constructor con inyección de dependencias
        public SalasController(CineTecDbContext context)
        {
            _context = context;
        }

        // ============================
        // GET: api/salas
        // ============================
        // Obtiene todas las salas junto con su sucursal asociada
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var salas = await _context.Salas
                .Include(s => s.Sucursal) // Incluye la relación con Sucursal
                .ToListAsync();

            return Ok(salas); // Retorna lista de salas
        }

        // ============================
        // POST: api/salas
        // ============================
        // Crea una nueva sala
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SalaDto dto)
        {
            // Verifica que la sucursal exista
            var sucursalExiste = await _context.Sucursales.AnyAsync(s => s.Id == dto.SucursalId);
            if (!sucursalExiste)
                return BadRequest(new { mensaje = "La sucursal no existe" });

            // Se crea la nueva sala
            var sala = new Sala
            {
                Identificador = dto.Identificador,
                SucursalId = dto.SucursalId,
                Filas = dto.Filas,
                Columnas = dto.Columnas,
                Capacidad = dto.Filas * dto.Columnas // Cálculo automático de capacidad
            };

            _context.Salas.Add(sala); // Se agrega a la BD
            await _context.SaveChangesAsync(); // Guarda cambios

            return Ok(new { mensaje = "Sala creada correctamente", sala });
        }

        // ============================
        // PUT: api/salas/{id}
        // ============================
        // Actualiza una sala existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SalaDto dto)
        {
            var sala = await _context.Salas.FindAsync(id); // Busca la sala

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            // Actualiza los datos
            sala.Identificador = dto.Identificador;
            sala.SucursalId = dto.SucursalId;
            sala.Filas = dto.Filas;
            sala.Columnas = dto.Columnas;
            sala.Capacidad = dto.Filas * dto.Columnas; // Recalcula capacidad

            await _context.SaveChangesAsync(); // Guarda cambios

            return Ok(new { mensaje = "Sala actualizada correctamente", sala });
        }

        // ============================
        // DELETE: api/salas/{id}
        // ============================
        // Elimina una sala
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _context.Salas.FindAsync(id); // Busca la sala

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            _context.Salas.Remove(sala); // Elimina de la BD
            await _context.SaveChangesAsync(); // Guarda cambios

            return Ok(new { mensaje = "Sala eliminada correctamente" });
        }
    }
}