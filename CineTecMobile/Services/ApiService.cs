using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CineTecMobile.Services;

/// <summary>
/// Servicio centralizado para todas las llamadas HTTP al API
/// </summary>
public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;

    // URL base configurable - se obtiene de preferencias
    // Para emulador Android: Usar IP real de la PC (NO localhost)
    // Para celular físico: Usar IP real de la PC en la misma red WiFi
    private const string DefaultBaseApiUrl = "http://172.18.92.169:5000/api";

    private string GetBaseApiUrl()
    {
        // Intentar obtener la URL guardada en preferencias
        var savedUrl = Preferences.Get("api_base_url", "");
        if (!string.IsNullOrEmpty(savedUrl))
        {
            return savedUrl;
        }
        
        // Usar valor por defecto
        return DefaultBaseApiUrl;
    }

    public static void SetApiUrl(string url)
    {
        if (!url.EndsWith("/api"))
        {
            url = url.TrimEnd('/') + "/api";
        }
        Preferences.Set("api_base_url", url);
    }

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        // Configurar timeout
        _httpClient.Timeout = TimeSpan.FromSeconds(30);

        _logger.LogInformation($"ApiService inicializado");
    }

    /// <summary>
    /// Realiza un GET request al API
    /// </summary>
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var baseUrl = GetBaseApiUrl();
            var url = $"{baseUrl}{endpoint}";
            _logger.LogInformation($"GET Request: {url}");

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Response Status: {response.StatusCode}");
            _logger.LogInformation($"Response Content Length: {content.Length} bytes");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error {response.StatusCode}: {content}");
                throw new HttpRequestException($"Error {response.StatusCode}: {content}");
            }

            _logger.LogDebug($"Response Content: {(content.Length > 200 ? content.Substring(0, 200) + "..." : content)}");

            var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _logger.LogInformation($"GET Success: Deserializado correctamente a {typeof(T).Name}");
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError($"JSON Deserialization Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Realiza un POST request al API
    /// </summary>
    public async Task<T?> PostAsync<T>(string endpoint, object? data = null)
    {
        try
        {
            var baseUrl = GetBaseApiUrl();
            var url = $"{baseUrl}{endpoint}";
            _logger.LogInformation($"POST Request: {url}");

            HttpContent content = new StringContent(
                JsonSerializer.Serialize(data),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error {response.StatusCode}: {responseContent}");
                throw new HttpRequestException($"Error {response.StatusCode}: {responseContent}");
            }

            var result = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _logger.LogInformation($"POST Success: {url}");
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError($"JSON Deserialization Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            throw;
        }
    }
}
