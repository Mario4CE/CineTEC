import API from "./api";

export const obtenerSalas = async () => {
    const res = await API.get("/admin/salas");
    return res.data;
};

export const crearSala = async (payload) => {
    const res = await API.post("/admin/salas", payload);
    return res.data;
};

export const actualizarSala = async (id, payload) => {
    const res = await API.put(`/admin/salas/${id}`, payload);
    return res.data;
};

export const eliminarSalaService = async (id) => {
    const res = await API.delete(`/admin/salas/${id}`);
    return res.data;
};