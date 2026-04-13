/*
 Entrada: Datos de una película (nombre original, nombre comercial, imagen, duración, protagonistas, director, clasificación)
 Salida: Objeto PeliculaDto con las propiedades correspondientes a los datos de la película

*/

namespace CineTec.API.DTOs
{
    public class PeliculaDto
    {
        public string NombreOriginal { get; set; } = string.Empty; // Propiedad para el nombre original de la película, inicializada como cadena vacía
        public string NombreComercial { get; set; } = string.Empty; // Propiedad para el nombre comercial de la película, inicializada como cadena vacía
        public string? Imagen { get; set; } // Propiedad para la URL de la imagen de la película, puede ser nula
        public int Duracion { get; set; } // Propiedad para la duración de la película en minutos
        public string? Protagonistas { get; set; } // Propiedad para los protagonistas de la película, puede ser nula
        public string? Director { get; set; } // Propiedad para el director de la película, puede ser nula
        public string? Clasificacion { get; set; } // Propiedad para la clasificación de la película, puede ser nula
    }
}
