
/* 
Entrada: Ninguna
Salida: Objeto Administrador
Restricciones: Ninguna
Descripción: Define la estructura de un administrador dentro del sistema CineTec, 
incluyendo sus datos básicos para autenticación y control de acceso
Modificado por: Mario
*/

namespace CineTec.API.Models
{
    public class Administrador
    {
        // Propiedades del administrador

        public int Id { get; set; } // Identificador único del administrador
        public string Nombre { get; set; } = string.Empty; // Nombre del administrador
        public string Usuario { get; set; } = string.Empty; // Nombre de usuario para autenticación
        public string Contrasena { get; set; } = string.Empty; // Contraseña
        public string Rol { get; set; } = "Admin"; // Rol del administrador, por defecto "Admin"
    }
}
