namespace CineTec.API.Models
{
    /// <summary>
    /// Modelo que representa a un usuario/cliente en el sistema CineTec
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public required string Apellido { get; set; }

        /// <summary>
        /// Cédula de identidad (única)
        /// </summary>
        public required string Cedula { get; set; }

        /// <summary>
        /// Número de teléfono de contacto
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Fecha de nacimiento del usuario
        /// </summary>
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Indica si el usuario está activo
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Calcula la edad del usuario basada en su fecha de nacimiento
        /// </summary>
        /// <returns>Edad en años</returns>
        public int ObtenerEdad()
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - FechaNacimiento.Year;

            // Restar 1 si el cumpleaños aún no ha ocurrido este año
            if (FechaNacimiento.Date > hoy.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }
    }
}
