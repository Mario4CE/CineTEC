/*
 * Entradas: 
  - Nombre: string (nombre de la sucursal)
  - Ubicacion: string (ubicación de la sucursal)
  - CantidadSalas: int (cantidad de salas en la sucursal)
 Salidas:
  - Un objeto JSON con las propiedades "Nombre", "Ubicacion" y "CantidadSalas" que representan la información de la sucursal.
 Restricciones:
  - El campo "Nombre" no puede estar vacío.
  - El campo "Ubicacion" no puede estar vacío.
  - El campo "CantidadSalas" debe ser un número entero positivo.
Descrpccion: Este DTO (Data Transfer Object) se utiliza para transferir la información de una sucursal entre el cliente y el servidor. Contiene las propiedades necesarias para crear o actualizar una sucursal en la base de datos. 
                Es importante validar los datos antes de procesarlos para asegurar que cumplen con las restricciones establecidas.
Modificado por: Mario
*/
namespace CineTec.API.DTOs
{
    public class SucursalDto
    {
        public string Nombre { get; set; } = string.Empty; // El valor predeterminado es una cadena vacía para evitar problemas de nullabilidad
        public string Ubicacion { get; set; } = string.Empty; // El valor predeterminado es una cadena vacía para evitar problemas de nullabilidad
        public int CantidadSalas { get; set; } // No se asigna un valor predeterminado, ya que es un tipo de valor (int) y su valor predeterminado es 0
    }
}