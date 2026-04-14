import API from "./api";

export const obtenerPeliculas = async () => {
    const res = await API.get("/admin/peliculas");
    return res.data;
};

export const crearPelicula = async (payload) => {
    const res = await API.post("/admin/peliculas", payload);
    return res.data;
};

export const actualizarPelicula = async (id, payload) => {
    const res = await API.put(`/admin/peliculas/${id}`, payload);
    return res.data;
};

export const eliminarPeliculaService = async (id) => {
    const res = await API.delete(`/admin/peliculas/${id}`);
    return res.data;
};