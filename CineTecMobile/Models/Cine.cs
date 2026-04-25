namespace CineTecMobile.Models;

public class Cine
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ubicacion { get; set; }

    // Para mostrar bonito en el Picker
    public string NombreCompleto => $"{Nombre} - {Ubicacion}";
}