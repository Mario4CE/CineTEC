import API from "./api";

export const obtenerProyecciones = async () => {
    const res = await API.get("/admin/proyecciones");
    return res.data;
};

export const crearProyeccion = async (payload) => {
    const res = await API.post("/admin/proyecciones", payload);
    return res.data;
};

export const eliminarProyeccionService = async (id) => {
    const res = await API.delete(`/admin/proyecciones/${id}`);
    return res.data;
};