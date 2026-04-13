/*
 Entrada: Ninguna
 Salida: Objeto Sucursal
 Restricciones: Ninguna
 Descripción: Define la estructura de una sucursal dentro del sistema CineTec,
 Modificado por: Mario
 */

namespace CineTec.API.Models
{
    public class Sucursal
    {
        public int Id { get; set; } // Identificador único de la sucursal
        public string Nombre { get; set; } = string.Empty; // Nombre de la sucursal
        public string Ubicacion { get; set; } = string.Empty; // Ubicación de la sucursal (dirección, ciudad, etc.)
        public int CantidadSalas { get; set; } // Cantidad de salas disponibles en la sucursal
    }
}