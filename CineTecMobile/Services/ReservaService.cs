using CineTecMobile.Models;
using System.Text.Json;

namespace CineTecMobile.Services;

/// <summary>
/// Servicio para gestionar reservas de forma local (SQLite simulado con Preferences)
/// </summary>
public class ReservaService
{
    private const string RESERVAS_KEY = "reservas_json";

    /// <summary>
    /// Obtiene todas las reservas del usuario actual
    /// </summary>
    public List<Reserva> ObtenerReservasDelUsuario(string cedulaUsuario)
    {
        try
        {
            var json = Preferences.Get(RESERVAS_KEY, "[]");
            var todasLasReservas = JsonSerializer.Deserialize<List<Reserva>>(json) ?? new();
            
            return todasLasReservas.Where(r => r.CedulaUsuario == cedulaUsuario).ToList();
        }
        catch
        {
            return new();
        }
    }

    /// <summary>
    /// Guarda una nueva reserva
    /// </summary>
    public Reserva GuardarReserva(Reserva reserva)
    {
        try
        {
            // Generar número de factura único
            if (string.IsNullOrEmpty(reserva.NumeroFactura))
            {
                reserva.NumeroFactura = $"FAC-{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
            }

            reserva.FechaCreacion = DateTime.Now;
            reserva.Id = GenerarIdUnico();

            // Obtener todas las reservas
            var json = Preferences.Get(RESERVAS_KEY, "[]");
            var reservas = JsonSerializer.Deserialize<List<Reserva>>(json) ?? new();

            // Agregar la nueva reserva
            reservas.Add(reserva);

            // Guardar de vuelta
            var jsonActualizado = JsonSerializer.Serialize(reservas);
            Preferences.Set(RESERVAS_KEY, jsonActualizado);

            return reserva;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al guardar reserva: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene una reserva por su número de factura
    /// </summary>
    public Reserva? ObtenerPorNumeroFactura(string numeroFactura)
    {
        try
        {
            var json = Preferences.Get(RESERVAS_KEY, "[]");
            var reservas = JsonSerializer.Deserialize<List<Reserva>>(json) ?? new();
            
            return reservas.FirstOrDefault(r => r.NumeroFactura == numeroFactura);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Actualiza el estado de una reserva
    /// </summary>
    public bool ActualizarEstado(string numeroFactura, string nuevoEstado)
    {
        try
        {
            var json = Preferences.Get(RESERVAS_KEY, "[]");
            var reservas = JsonSerializer.Deserialize<List<Reserva>>(json) ?? new();

            var reserva = reservas.FirstOrDefault(r => r.NumeroFactura == numeroFactura);
            if (reserva == null) return false;

            reserva.Estado = nuevoEstado;

            var jsonActualizado = JsonSerializer.Serialize(reservas);
            Preferences.Set(RESERVAS_KEY, jsonActualizado);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Genera un ID único para la reserva
    /// </summary>
    private int GenerarIdUnico()
    {
        try
        {
            var json = Preferences.Get(RESERVAS_KEY, "[]");
            var reservas = JsonSerializer.Deserialize<List<Reserva>>(json) ?? new();
            
            return reservas.Any() ? reservas.Max(r => r.Id) + 1 : 1;
        }
        catch
        {
            return 1;
        }
    }
}
