/*
DESCRIPCIÓN:
Este archivo contiene las funciones para interactuar con la API del backend
relacionadas con la gestión de sucursales. Permite realizar operaciones CRUD
(crear, leer, actualizar y eliminar) mediante solicitudes HTTP.

ENTRADAS:
- ID de la sucursal (para actualizar y eliminar).
- Objeto payload con los datos de la sucursal (para crear y actualizar).

SALIDAS:
- Datos devueltos por el backend (res.data).
- Promesas que contienen la respuesta de cada operación.

RESTRICCIONES:
- Depende de la configuración de Axios en api.js.
- El backend debe estar activo y responder en las rutas definidas.
- Las rutas deben coincidir con los endpoints del backend.

Modificado por: Mario
*/

import API from "./api";

// Obtener todas las sucursales
export const obtenerSucursales = async () => {
    const res = await API.get("/admin/sucursales");
    return res.data;
};

// Crear una nueva sucursal
export const crearSucursal = async (payload) => {
    const res = await API.post("/admin/sucursales", payload);
    return res.data;
};

// Actualizar una sucursal existente
export const actualizarSucursal = async (id, payload) => {
    const res = await API.put(`/admin/sucursales/${id}`, payload);
    return res.data;
};

// Eliminar una sucursal
export const eliminarSucursalService = async (id) => {
    const res = await API.delete(`/admin/sucursales/${id}`);
    return res.data;
};