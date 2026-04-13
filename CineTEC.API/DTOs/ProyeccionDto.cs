/*
 Entrada: Datos de una proyección (ID de película, ID de sala, fecha y hora de la proyección)
 Salida: Objeto ProyeccionDto con las propiedades correspondientes a los datos de la proyección
 Restricciones: El ID de película y el ID de sala deben ser enteros válidos, la fecha debe ser un valor de tipo DateTime
 Descrpccion: Clase DTO (Data Transfer Object) que representa los datos necesarios para crear o actualizar una proyección en el sistema CineTec. 
              Contiene tres propiedades: PeliculaId para el ID de la película, SalaId para el ID de la sala, y Fecha para la fecha y hora de la proyección.
 Modificado por: Mario
*/

namespace CineTec.API.DTOs
{
    public class ProyeccionDto
    {
        public int PeliculaId { get; set; }
        public int SalaId { get; set; }
        public DateTime Fecha { get; set; }
    }
}