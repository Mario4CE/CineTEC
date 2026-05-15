namespace CineTecMobile.Models;

/// <summary>
/// Modelo que representa una reserva de boletos
/// </summary>
public class Reserva
{
    public int Id { get; set; }
    public string NumeroFactura { get; set; }
    public string CedulaUsuario { get; set; }
    public int CineId { get; set; }
    public string NombreCine { get; set; }
    public int PeliculaId { get; set; }
    public string NombrePelicula { get; set; }
    public int ProyeccionId { get; set; }
    public DateTime FechaProyeccion { get; set; }
    public string HoraProyeccion { get; set; }
    public List<string> Asientos { get; set; } = new(); // Ejemplo: ["A1", "A2", "B5"]
    public int CantidadAsientos { get; set; }
    public decimal PrecioUnitario { get; set; } // 8000
    public decimal TotalPago { get; set; }
    public string Estado { get; set; } = "Pendiente"; // Pendiente, Confirmada, Cancelada
    public DateTime FechaCreacion { get; set; }
    public string SalaNumero { get; set; }
}
