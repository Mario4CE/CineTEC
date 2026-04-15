
 /*
 DESCRIPCIÓN:
 Este archivo configura una instancia de Axios para realizar solicitudes HTTP
 al backend del sistema. Centraliza la URL base de la API para facilitar el uso
 en los servicios del proyecto.
 
 ENTRADAS:
 - Parámetros enviados en las solicitudes HTTP (GET, POST, PUT, DELETE).
 - Datos que se envían al backend desde los servicios.
 
 SALIDAS:
 - Respuestas del backend (datos, estados HTTP, errores).
 - Promesas que son consumidas por los servicios del sistema.
 
 RESTRICCIONES:
 - La URL base debe coincidir con la del backend en ejecución.
-- Requiere que el backend esté activo en "http://localhost:5000/api".
+- Puede configurarse con la variable de entorno VITE_API_URL.
+- Si no existe VITE_API_URL, usa "http://localhost:5000/api".
 - Depende de la librería axios.
 
 Modificado por: Mario
 */
 
 import axios from "axios";

 const apiBaseUrl = import.meta.env.VITE_API_URL || "http://localhost:5000/api";
 // Creación de una instancia de Axios con configuración base
 const API = axios.create({
    baseURL: apiBaseUrl
 });

// Exportación de la instancia para ser usada en los servicios
export default API;