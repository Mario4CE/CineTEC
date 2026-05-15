using CineTecMobile.Models;
using Microsoft.Extensions.Logging;

namespace CineTecMobile.Services;

/// <summary>
/// Servicio específico para operaciones de usuarios
/// </summary>
public class UsuarioService
{
    private readonly ApiService _apiService;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(ApiService apiService, ILogger<UsuarioService> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene un usuario por su cédula
    /// </summary>
    public async Task<Usuario?> ObtenerPorCedula(string cedula)
    {
        _logger.LogInformation($"Buscando usuario con cédula: {cedula}");
        
        try
        {
            var usuario = await _apiService.GetAsync<Usuario>($"/usuarios/cedula/{cedula}");
            _logger.LogInformation($"Usuario encontrado: {usuario?.NombreCompleto}");
            return usuario;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener usuario: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Registra un nuevo usuario
    /// </summary>
    public async Task<Usuario?> Registrar(object usuarioDto)
    {
        _logger.LogInformation("Registrando nuevo usuario");
        
        try
        {
            var usuario = await _apiService.PostAsync<Usuario>("/usuarios/registrar", usuarioDto);
            _logger.LogInformation($"Usuario registrado exitosamente: {usuario?.NombreCompleto}");
            return usuario;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al registrar usuario: {ex.Message}");
            throw;
        }
    }
}
