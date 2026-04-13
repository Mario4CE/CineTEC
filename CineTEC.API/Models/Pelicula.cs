/*
 Entrada: Ninguna
 Salida: Objeto Pelicula
 Restricciones: Ninguna
 Descripción: Define la estructura de una película dentro del sistema CineTec,
 Modificado por: Mario
 */

namespace CineTec.API.Models
{
    public class Pelicula
    {
        public int Id { get; set; } // Identificador único de la película
        public string NombreOriginal { get; set; } = string.Empty; // Nombre original de la película
        public string NombreComercial { get; set; } = string.Empty; // Nombre comercial de la película
        public string? Imagen { get; set; } // URL o ruta de la imagen de la película
        public int Duracion { get; set; } // Duración de la película en minutos
        public string? Protagonistas { get; set; } // Lista de protagonistas de la película
        public string? Director { get; set; } // Nombre del director de la película
        public string? Clasificacion { get; set; } // Clasificación de la película (e.g., "PG-13", "R", etc.)
    }
}