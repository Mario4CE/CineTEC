using CineTecMobile.Models;
using Microsoft.Extensions.Logging;

namespace CineTecMobile.Services;

/// <summary>
/// Servicio para operaciones relacionadas con cines
/// </summary>
public class CineService
{
    private readonly ApiService _apiService;
    private readonly ILogger<CineService> _logger;

    public CineService(ApiService apiService, ILogger<CineService> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene la lista de cines/sucursales
    /// </summary>
    public async Task<List<Cine>> GetCines()
    {
        _logger.LogInformation("Obteniendo lista de cines desde el API");

        try
        {
            // Endpoint: /api/admin/Sucursales
            var cines = await _apiService.GetAsync<List<Cine>>("/admin/Sucursales");
            _logger.LogInformation($"Se obtuvieron {cines?.Count ?? 0} cines correctamente");
            return cines ?? new List<Cine>();
        }
        catch (HttpRequestException hre)
        {
            _logger.LogError($"Error de conexión HTTP al obtener cines: {hre.Message}");
            throw;
        }
        catch (InvalidOperationException ioe)
        {
            _logger.LogError($"Error de configuración al obtener cines: {ioe.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error inesperado al obtener cines: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
}