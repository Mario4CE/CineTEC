public class Proyeccion
{
    public int Id { get; set; }
    public int PeliculaId { get; set; }
    public int SalaId { get; set; }
    public DateTime Fecha { get; set; }

    public Pelicula Pelicula { get; set; }
    public Sala Sala { get; set; }

    // Para UI
    public string HoraFormateada { get; set; }
    public string FechaFormateada { get; set; }
    public string SalaInfo { get; set; }
}