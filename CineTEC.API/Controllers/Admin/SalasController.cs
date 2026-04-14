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
    [ApiController]
    [Route("api/admin/[controller]")]
    public class SalasController : ControllerBase
    {
        private readonly CineTecDbContext _context;

        public SalasController(CineTecDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var salas = await _context.Salas
                .Include(s => s.Sucursal)
                .ToListAsync();

            return Ok(salas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sala = await _context.Salas
                .Include(s => s.Sucursal)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            return Ok(sala);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SalaDto dto)
        {
            var sucursalExiste = await _context.Sucursales.AnyAsync(s => s.Id == dto.SucursalId);
            if (!sucursalExiste)
                return BadRequest(new { mensaje = "La sucursal no existe" });

            var sala = new Sala
            {
                Identificador = dto.Identificador,
                SucursalId = dto.SucursalId,
                Filas = dto.Filas,
                Columnas = dto.Columnas,
                Capacidad = dto.Filas * dto.Columnas
            };

            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = sala.Id }, sala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SalaDto dto)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            var sucursalExiste = await _context.Sucursales.AnyAsync(s => s.Id == dto.SucursalId);
            if (!sucursalExiste)
                return BadRequest(new { mensaje = "La sucursal no existe" });

            sala.Identificador = dto.Identificador;
            sala.SucursalId = dto.SucursalId;
            sala.Filas = dto.Filas;
            sala.Columnas = dto.Columnas;
            sala.Capacidad = dto.Filas * dto.Columnas;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sala actualizada correctamente", sala });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sala eliminada correctamente" });
        }
    }
}