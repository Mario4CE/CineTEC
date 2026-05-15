public class Usuario
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }

    /// <summary>
    /// Propiedad calculada que concatena Nombre y Apellido
    /// </summary>
    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
}