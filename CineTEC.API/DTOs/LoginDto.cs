/*
 Entrada: Datos de inicio de sesión (nombre de usuario y contraseña)
 Salida: Objeto LoginDto con las propiedades Usuario y Contrasena
 Restricciones: Las propiedades Usuario y Contrasena deben ser cadenas no nulas
 Descripción: Clase DTO (Data Transfer Object) que representa los datos necesarios para el proceso de inicio de sesión en el sistema CineTec. 
             Contiene dos propiedades: Usuario para el nombre de usuario y Contrasena para la contraseña, ambas inicializadas como cadenas vacías.
  Modificado por: Mario

*/

namespace CineTec.API.DTOs
{
    public class LoginDto
    {
        public string Usuario { get; set; } = string.Empty; // Propiedad para el nombre de usuario, inicializada como cadena vacía
        public string Contrasena { get; set; } = string.Empty; // Propiedad para la contraseña, inicializada como cadena vacía
    }
}