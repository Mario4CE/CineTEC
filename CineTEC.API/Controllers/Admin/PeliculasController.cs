/*
 Entradas disponibles:
  GET    /api/admin/peliculas
  GET    /api/admin/peliculas/{id}
  POST   /api/admin/peliculas
  PUT    /api/admin/peliculas/{id}
  DELETE /api/admin/peliculas/{id}

 Este controller pertenece a la vista de administrador.
 Su función es gestionar las películas del sistema:
 - consultar todas
 - consultar una por id
 - crear
 - actualizar
 - eliminar
Salida: Respuestas en formato JSON con el resultado de cada operación, incluyendo mensajes de éxito o error según corresponda.
Restricciones: Solo los administradores pueden acceder a estas rutas. 
                Se deben realizar validaciones básicas para asegurar que los datos ingresados sean correctos 
Descripción: Este controlador permite a los administradores realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre las películas registradas en el sistema. 
             Cada método maneja una ruta específica y realiza validaciones básicas para asegurar la integridad de los datos.
Modificado por: Mario
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using CineTec.API.Data;
using CineTec.API.DTOs;
using CineTec.API.Models;

namespace CineTec.API.Controllers.Admin
{
    // Indica que esta clase es un controlador de API
    [ApiController]

    // Define la ruta base de este controlador
    // Como está en administrador, la ruta queda:
    // /api/admin/peliculas
    [Route("api/admin/[controller]")]
    public class PeliculasController : ControllerBase
    {
        // Contexto de base de datos para acceder a las tablas
        private readonly CineTecDbContext _context;
        private readonly IWebHostEnvironment _environment;

        // Constructor del controlador
        // Aquí se inyecta el DbContext para poder usar la base de datos
        public PeliculasController(CineTecDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // ============================================
        // GET: /api/admin/peliculas
        // Obtiene la lista completa de películas
        // ============================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Trae todas las películas de la tabla Peliculas
            var peliculas = await _context.Peliculas.ToListAsync();

            // Retorna código 200 con la lista
            return Ok(peliculas);
        }

        // ============================================
        // GET: /api/admin/peliculas/{id}
        // Obtiene una película específica por su id
        // ============================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Busca la película por llave primaria
            var pelicula = await _context.Peliculas.FindAsync(id);

            // Si no existe, retorna 404
            if (pelicula == null)
            {
                return NotFound(new { mensaje = "Película no encontrada" });
            }

            // Si existe, retorna 200 con la película
            return Ok(pelicula);
        }


        // ============================================
        // POST: /api/admin/peliculas/upload-imagen
        // Sube una imagen y devuelve la ruta pública
        // ============================================
        [HttpPost("upload-imagen")]
        public async Task<IActionResult> UploadImagen([FromForm] IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest(new { mensaje = "Debe seleccionar una imagen" });
            }

            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();

            if (!extensionesPermitidas.Contains(extension))
            {
                return BadRequest(new { mensaje = "Formato no permitido. Use JPG, PNG o WEBP" });
            }

            var carpetaImagenes = Path.Combine(
                _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                "images",
                "peliculas"
            );

            Directory.CreateDirectory(carpetaImagenes);

            // Nombre original sin extensión
            var nombreOriginal = Path.GetFileNameWithoutExtension(archivo.FileName);

            // Limpiar caracteres inválidos
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                nombreOriginal = nombreOriginal.Replace(c, '_');
            }

            // Agregar un GUID para que no se repita
            var nombreArchivo = $"{nombreOriginal}{extension}";
            var rutaFisica = Path.Combine(carpetaImagenes, nombreArchivo);

            await using var stream = new FileStream(rutaFisica, FileMode.Create);
            await archivo.CopyToAsync(stream);

            var rutaPublica = $"/images/peliculas/{nombreArchivo}";

            return Ok(new
            {
                mensaje = "Imagen subida correctamente",
                ruta = rutaPublica
            });
        }

        // ============================================
        // POST: /api/admin/peliculas
        // Crea una nueva película
        // ============================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PeliculaDto dto)
        {
            // Validación básica:
            // El nombre comercial es obligatorio
            if (string.IsNullOrWhiteSpace(dto.NombreComercial))
            {
                return BadRequest(new { mensaje = "El nombre comercial es obligatorio" });
            }

            // Validación básica:
            // El nombre original es obligatorio
            if (string.IsNullOrWhiteSpace(dto.NombreOriginal))
            {
                return BadRequest(new { mensaje = "El nombre original es obligatorio" });
            }

            // Validación básica:
            // La duración debe ser mayor que cero
            if (dto.Duracion <= 0)
            {
                return BadRequest(new { mensaje = "La duración debe ser mayor que cero" });
            }

            // Se crea un nuevo objeto Pelicula a partir del DTO recibido
            var pelicula = new Pelicula
            {
                NombreOriginal = dto.NombreOriginal,
                NombreComercial = dto.NombreComercial,
                Imagen = dto.Imagen,
                Duracion = dto.Duracion,
                Protagonistas = dto.Protagonistas,
                Director = dto.Director,
                Clasificacion = dto.Clasificacion
            };

            // Se agrega la nueva película al contexto
            _context.Peliculas.Add(pelicula);

            // Se guardan los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Retorna 201 indicando que se creó correctamente
            // además devuelve la ruta para consultar esa película
            return CreatedAtAction(nameof(GetById), new { id = pelicula.Id }, pelicula);
        }

        // ============================================
        // PUT: /api/admin/peliculas/{id}
        // Actualiza una película existente
        // ============================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PeliculaDto dto)
        {
            // Busca la película actual en la base de datos
            var pelicula = await _context.Peliculas.FindAsync(id);

            // Si no existe, retorna 404
            if (pelicula == null)
            {
                return NotFound(new { mensaje = "Película no encontrada" });
            }

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreComercial))
            {
                return BadRequest(new { mensaje = "El nombre comercial es obligatorio" });
            }

            if (string.IsNullOrWhiteSpace(dto.NombreOriginal))
            {
                return BadRequest(new { mensaje = "El nombre original es obligatorio" });
            }

            if (dto.Duracion <= 0)
            {
                return BadRequest(new { mensaje = "La duración debe ser mayor que cero" });
            }

            // Se actualizan los campos de la película
            pelicula.NombreOriginal = dto.NombreOriginal;
            pelicula.NombreComercial = dto.NombreComercial;
            pelicula.Imagen = dto.Imagen;
            pelicula.Duracion = dto.Duracion;
            pelicula.Protagonistas = dto.Protagonistas;
            pelicula.Director = dto.Director;
            pelicula.Clasificacion = dto.Clasificacion;

            // Se guardan los cambios
            await _context.SaveChangesAsync();

            // Retorna 200 indicando actualización exitosa
            return Ok(new
            {
                mensaje = "Película actualizada correctamente",
                pelicula
            });
        }

        // ============================================
        // DELETE: /api/admin/peliculas/{id}
        // Elimina una película por id
        // ============================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Busca la película que se desea eliminar
            var pelicula = await _context.Peliculas.FindAsync(id);

            // Si no existe, retorna 404
            if (pelicula == null)
            {
                return NotFound(new { mensaje = "Película no encontrada" });
            }

            // Se elimina la película del contexto
            _context.Peliculas.Remove(pelicula);

            // Se guardan los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Retorna 200 indicando eliminación correcta
            return Ok(new { mensaje = "Película eliminada correctamente" });
        }
    }
}