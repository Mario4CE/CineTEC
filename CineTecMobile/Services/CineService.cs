using System.Text.Json;
using CineTecMobile.Models;

namespace CineTecMobile.Services;

public class CineService
{
    private readonly HttpClient _httpClient;

    public CineService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Cine>> GetCines()
    {
        string url = "http://192.168.100.21:5000/api/Sucursales";

        var response = await _httpClient.GetStringAsync(url);

        return JsonSerializer.Deserialize<List<Cine>>(response);
    }
}