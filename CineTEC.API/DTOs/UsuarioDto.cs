namespace CineTec.API.DTOs
{
    /// <summary>
    /// DTO para crear o actualizar un usuario
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// Identificador único (solo para actualizaciones)
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string Apellido { get; set; }

        /// <summary>
        /// Cédula de identidad
        /// </summary>
        public string Cedula { get; set; }

        /// <summary>
        /// Número de teléfono
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime FechaNacimiento { get; set; }
    }

    /// <summary>
    /// DTO para respuestas de usuario con edad calculada
    /// </summary>
    public class UsuarioResponseDto
    {
        /// <summary>
        /// Identificador único
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo (Nombre + Apellido)
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Cédula de identidad
        /// </summary>
        public string Cedula { get; set; }

        /// <summary>
        /// Número de teléfono
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Edad calculada en años
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Indica si el usuario está activo
        /// </summary>
        public bool Activo { get; set; }
    }
}
