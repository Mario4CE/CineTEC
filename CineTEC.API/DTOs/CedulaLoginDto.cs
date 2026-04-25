namespace CineTec.API.DTOs
{
    /// <summary>
    /// DTO para login de usuario por cédula
    /// </summary>
    public class CedulaLoginDto
    {
        /// <summary>
        /// Cédula de identidad del usuario
        /// </summary>
        public string Cedula { get; set; } = string.Empty;
    }
}
