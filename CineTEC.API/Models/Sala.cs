/*
 Entrada: Ninguna
 Salida: Objeto Sala
 Restricciones: Ninguna
 Descripción: Define la estructura de una sala dentro del sistema CineTec, incluyendo su capacidad y relación con la sucursal a la que pertenece
 Modificado por: Mario
 */

namespace CineTec.API.Models
{
    public class Sala
    {
        public int Id { get; set; } // Identificador único de la sala
        public string Identificador { get; set; } = string.Empty; // Identificador de la sala (e.g., "Sala 1", "Sala A", etc.)
        public int SucursalId { get; set; } // Identificador de la sucursal a la que pertenece la sala
        public int Filas { get; set; } // Cantidad de filas en la sala
        public int Columnas { get; set; } // Cantidad de columnas en la sala
        public int Capacidad { get; set; } // Capacidad total de la sala (calculada como Filas * Columnas)

        public Sucursal? Sucursal { get; set; } 
    }
}
