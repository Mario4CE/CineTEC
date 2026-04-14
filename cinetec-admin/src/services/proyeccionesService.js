/*
DESCRIPCIÓN:
Este archivo contiene las funciones para interactuar con la API del backend
relacionadas con la gestión de proyecciones. Permite obtener, crear y eliminar
proyecciones mediante solicitudes HTTP.

ENTRADAS:
- ID de la proyección (para eliminar).
- Objeto payload con los datos de la proyección (peliculaId, salaId, fecha).

SALIDAS:
- Datos devueltos por el backend (res.data).
- Promesas con la respuesta de cada operación.

RESTRICCIONES:
- Depende de la configuración de Axios en api.js.
- El backend debe estar activo y responder en las rutas definidas.
- Las rutas deben coincidir con los endpoints del backend.

Modificado por: Mario
*/

import API from "./api";

// Obtener todas las proyecciones
export const obtenerProyecciones = async () => {
    const res = await API.get("/admin/proyecciones");
    return res.data;
};

// Crear una nueva proyección
export const crearProyeccion = async (payload) => {
    const res = await API.post("/admin/proyecciones", payload);
    return res.data;
};

// Eliminar una proyección
export const eliminarProyeccionService = async (id) => {
    const res = await API.delete(`/admin/proyecciones/${id}`);
    return res.data;
};