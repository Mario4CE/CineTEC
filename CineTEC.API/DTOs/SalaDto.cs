/*
 Entrada: DTO para la creación de una sala en el sistema de gestión de cines.
 Salida: Un objeto SalaDto que contiene la información necesaria para crear una sala,
            incluyendo el identificador de la sala, el ID de la sucursal a la que pertenece, y las dimensiones de la sala (número de filas y columnas).
Restricciones: El identificador de la sala debe ser único dentro de la sucursal, y las dimensiones de la sala deben ser positivas (filas > 0 y columnas > 0).
Descrpcion: Este DTO se utiliza para transferir los datos necesarios para crear una nueva sala en el sistema. El identificador de la sala es un string que puede ser algo como "Sala 1", "Sala 2", etc., y debe ser único dentro de la sucursal a la que pertenece.
            El ID de la sucursal es un entero que hace referencia a la sucursal a la que se asignará la sala.
            Las filas y columnas representan la disposición de los asientos en la sala, y ambos deben ser valores positivos para garantizar una configuración válida de la sala.
Modificado por: Mario
*/
namespace CineTec.API.DTOs
{
    public class SalaDto
    {
        public string Identificador { get; set; } = string.Empty; // Ejemplo: "Sala 1", "Sala 2", etc.
        public int SucursalId { get; set; } // ID de la sucursal a la que pertenece la sala
        public int Filas { get; set; } // Número de filas en la sala
        public int Columnas { get; set; } // Número de columnas en la sala
    }
}