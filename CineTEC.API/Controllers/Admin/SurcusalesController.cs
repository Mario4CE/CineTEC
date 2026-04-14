/* 
Entrada: Solicitudes HTTP (GET, POST, PUT, DELETE)
Salida: Respuestas HTTP con datos o mensajes
Restricciones: La base de datos debe estar configurada y accesible
Descripción: Controlador encargado de gestionar las operaciones CRUD 
(Crear, Leer, Actualizar y Eliminar) de las sucursales del sistema CineTec
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
    public class SucursalesController : ControllerBase
    {
        private readonly CineTecDbContext _context;

        public SucursalesController(CineTecDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sucursales = await _context.Sucursales.ToListAsync();
            return Ok(sucursales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);

            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });

            return Ok(sucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SucursalDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre es obligatorio" });

            if (string.IsNullOrWhiteSpace(dto.Ubicacion))
                return BadRequest(new { mensaje = "La ubicación es obligatoria" });

            if (dto.CantidadSalas < 0)
                return BadRequest(new { mensaje = "La cantidad de salas no puede ser negativa" });

            var sucursal = new Sucursal
            {
                Nombre = dto.Nombre,
                Ubicacion = dto.Ubicacion,
                CantidadSalas = dto.CantidadSalas
            };

            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = sucursal.Id }, sucursal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SucursalDto dto)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);

            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre es obligatorio" });

            if (string.IsNullOrWhiteSpace(dto.Ubicacion))
                return BadRequest(new { mensaje = "La ubicación es obligatoria" });

            if (dto.CantidadSalas < 0)
                return BadRequest(new { mensaje = "La cantidad de salas no puede ser negativa" });

            sucursal.Nombre = dto.Nombre;
            sucursal.Ubicacion = dto.Ubicacion;
            sucursal.CantidadSalas = dto.CantidadSalas;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sucursal actualizada correctamente", sucursal });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);

            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sucursal eliminada correctamente" });
        }
    }
}