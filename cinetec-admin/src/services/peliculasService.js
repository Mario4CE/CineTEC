/*
DESCRIPCIÓN:
Este archivo contiene las funciones para interactuar con la API del backend
relacionadas con la gestión de películas. Permite realizar operaciones CRUD
(crear, leer, actualizar y eliminar) mediante solicitudes HTTP.

ENTRADAS:
- ID de la película (para actualizar y eliminar).
- Objeto payload con los datos de la película 
  (nombreOriginal, nombreComercial, imagen, duracion, protagonistas, director, clasificacion).

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

// Obtener todas las películas
export const obtenerPeliculas = async () => {
    const res = await API.get("/admin/peliculas");
    return res.data;
};

// Crear una nueva película
export const crearPelicula = async (payload) => {
    const res = await API.post("/admin/peliculas", payload);
    return res.data;
};

// Actualizar una película existente
export const actualizarPelicula = async (id, payload) => {
    const res = await API.put(`/admin/peliculas/${id}`, payload);
    return res.data;
};

// Eliminar una película
export const eliminarPeliculaService = async (id) => {
    const res = await API.delete(`/admin/peliculas/${id}`);
    return res.data;
};