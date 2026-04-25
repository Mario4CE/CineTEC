/*
 Entrada: DTOs de Usuario (UsuarioDto, credenciales, etc.)
 Salida: Usuario creado/verificado, booleanos de validación
 Restricciones: Validar que la cédula sea única, validar datos requeridos
 Descripción: Servicio que contiene la lógica de negocio para gestionar usuarios/clientes
 Modificado por: Mario
*/

using CineTec.API.Data;
using CineTec.API.Models;
using CineTec.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CineTec.API.Services
{
    public class UsuarioService
    {
        private readonly CineTecDbContext _context;

        public UsuarioService(CineTecDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema
        /// </summary>
        /// <param name="dto">Datos del usuario a registrar</param>
        /// <returns>Usuario registrado o excepción si la cédula ya existe</returns>
        public async Task<UsuarioResponseDto> RegistrarUsuario(UsuarioDto dto)
        {
            // Validar que la cédula no exista ya
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Cedula == dto.Cedula);

            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("La cédula ya está registrada en el sistema");
            }

            // Validar fechas
            if (dto.FechaNacimiento >= DateTime.Today)
            {
                throw new InvalidOperationException("La fecha de nacimiento no puede ser en el futuro");
            }

            // Crear nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre?.Trim(),
                Apellido = dto.Apellido?.Trim(),
                Cedula = dto.Cedula?.Trim(),
                Telefono = dto.Telefono?.Trim(),
                FechaNacimiento = dto.FechaNacimiento,
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            // Guardar en base de datos
            await _context.Usuarios.AddAsync(nuevoUsuario);
            await _context.SaveChangesAsync();

            // Retornar respuesta
            return MapearUsuarioAResponse(nuevoUsuario);
        }

        /// <summary>
        /// Verifica las credenciales de un usuario
        /// </summary>
        /// <param name="cedula">Cédula del usuario</param>
        /// <returns>Usuario si existe y está activo, null si no</returns>
        public async Task<UsuarioResponseDto> VerificarUsuario(string cedula)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Cedula == cedula && u.Activo);

            if (usuario == null)
            {
                return null;
            }

            return MapearUsuarioAResponse(usuario);
        }

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Usuario encontrado o null</returns>
        public async Task<UsuarioResponseDto> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id && u.Activo);

            if (usuario == null)
            {
                return null;
            }

            return MapearUsuarioAResponse(usuario);
        }

        /// <summary>
        /// Obtiene un usuario por su cédula
        /// </summary>
        /// <param name="cedula">Cédula del usuario</param>
        /// <returns>Usuario encontrado o null</returns>
        public async Task<UsuarioResponseDto> ObtenerUsuarioPorCedula(string cedula)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Cedula == cedula && u.Activo);

            if (usuario == null)
            {
                return null;
            }

            return MapearUsuarioAResponse(usuario);
        }

        /// <summary>
        /// Actualiza datos de un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="dto">Nuevos datos</param>
        /// <returns>Usuario actualizado o excepción</returns>
        public async Task<UsuarioResponseDto> ActualizarUsuario(int id, UsuarioDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id && u.Activo);

            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            // Validar cédula única (si se intenta cambiar)
            if (usuario.Cedula != dto.Cedula)
            {
                var cedulaDuplicada = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Cedula == dto.Cedula && u.Id != id);

                if (cedulaDuplicada != null)
                {
                    throw new InvalidOperationException("La cédula ya está registrada por otro usuario");
                }
            }

            // Actualizar datos
            usuario.Nombre = dto.Nombre?.Trim() ?? usuario.Nombre;
            usuario.Apellido = dto.Apellido?.Trim() ?? usuario.Apellido;
            usuario.Cedula = dto.Cedula?.Trim() ?? usuario.Cedula;
            usuario.Telefono = dto.Telefono?.Trim() ?? usuario.Telefono;
            usuario.FechaNacimiento = dto.FechaNacimiento;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return MapearUsuarioAResponse(usuario);
        }

        /// <summary>
        /// Desactiva un usuario (eliminación lógica)
        /// </summary>
        /// <param name="id">ID del usuario</param>
        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            usuario.Activo = false;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Obtiene todos los usuarios activos
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public async Task<List<UsuarioResponseDto>> ObtenerTodosLosUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Where(u => u.Activo)
                .ToListAsync();

            return usuarios.Select(MapearUsuarioAResponse).ToList();
        }

        /// <summary>
        /// Mapea un Usuario a UsuarioResponseDto
        /// </summary>
        private UsuarioResponseDto MapearUsuarioAResponse(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
                Cedula = usuario.Cedula,
                Telefono = usuario.Telefono,
                Edad = usuario.ObtenerEdad(),
                FechaNacimiento = usuario.FechaNacimiento,
                Activo = usuario.Activo
            };
        }
    }
}
