/*
DESCRIPCIÓN:
Este archivo contiene las funciones para interactuar con la API del backend
relacionadas con la gestión de salas. Permite realizar operaciones CRUD
(crear, leer, actualizar y eliminar) mediante solicitudes HTTP.

ENTRADAS:
- ID de la sala (para actualizar y eliminar).
- Objeto payload con los datos de la sala (identificador, sucursalId, filas, columnas).

SALIDAS:
- Datos devueltos por el backend (res.data).
- Promesas con la respuesta de cada operación.

RESTRICCIONES:
- Depende de la configuración de Axios en api.js.
- El backend debe estar activo en las rutas definidas.
- Las rutas deben coincidir con los endpoints del backend.
*/

import API from "./api";

// Obtener todas las salas
export const obtenerSalas = async () => {
    const res = await API.get("/admin/salas");
    return res.data;
};

// Crear una nueva sala
export const crearSala = async (payload) => {
    const res = await API.post("/admin/salas", payload);
    return res.data;
};

// Actualizar una sala existente
export const actualizarSala = async (id, payload) => {
    const res = await API.put(`/admin/salas/${id}`, payload);
    return res.data;
};

// Eliminar una sala
export const eliminarSalaService = async (id) => {
    const res = await API.delete(`/admin/salas/${id}`);
    return res.data;
};