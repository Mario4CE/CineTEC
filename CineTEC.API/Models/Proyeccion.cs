/*
 Entrada: Ninguna
 Salida: Objeto Proyeccion
 Restricciones: Ninguna
 Descripción: Define la estructura de una proyección dentro del sistema CineTec
 Modificado por: Mario
*/
namespace CineTec.API.Models
{
    public class Proyeccion
    {
        public int Id { get; set; } // Identificador único de la proyección
        public int PeliculaId { get; set; } // Identificador de la película que se proyectará
        public int SalaId { get; set; } // Identificador de la sala donde se proyectará la película
        public DateTime Fecha { get; set; } // Fecha y hora de la proyección

        public Pelicula? Pelicula { get; set; } // Propiedad de navegación para acceder a los detalles de la película asociada a la proyección
        public Sala? Sala { get; set; }
    }
}
