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
    // Indica que es un controlador de API
    [ApiController]

    // Ruta base: api/sucursales
    [Route("api/[controller]")]
    public class SucursalesController : ControllerBase
    {
        // Contexto de base de datos
        private readonly CineTecDbContext _context;

        // Constructor que recibe el contexto
        public SucursalesController(CineTecDbContext context)
        {
            _context = context;
        }

        // GET: api/sucursales
        // Obtiene todas las sucursales
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sucursales = await _context.Sucursales.ToListAsync();
            return Ok(sucursales);
        }

        // POST: api/sucursales
        // Crea una nueva sucursal
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SucursalDto dto)
        {
            // Se crea una nueva instancia de Sucursal a partir del DTO
            var sucursal = new Sucursal
            {
                Nombre = dto.Nombre,
                Ubicacion = dto.Ubicacion,
                CantidadSalas = dto.CantidadSalas
            };

            // Se agrega a la base de datos
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            // Respuesta exitosa
            return Ok(new { mensaje = "Sucursal creada correctamente", sucursal });
        }

        // PUT: api/sucursales/{id}
        // Actualiza una sucursal existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SucursalDto dto)
        {
            // Busca la sucursal por ID
            var sucursal = await _context.Sucursales.FindAsync(id);

            // Si no existe, retorna error 404
            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });

            // Actualiza los datos
            sucursal.Nombre = dto.Nombre;
            sucursal.Ubicacion = dto.Ubicacion;
            sucursal.CantidadSalas = dto.CantidadSalas;

            // Guarda cambios
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sucursal actualizada correctamente", sucursal });
        }

        // DELETE: api/sucursales/{id}
        // Elimina una sucursal
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Busca la sucursal
            var sucursal = await _context.Sucursales.FindAsync(id);

            // Si no existe, retorna error 404
            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });

            // Elimina la sucursal
            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sucursal eliminada correctamente" });
        }
    }
}