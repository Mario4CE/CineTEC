import API from "./api";

export const obtenerSucursales = async () => {
    const res = await API.get("/admin/sucursales");
    return res.data;
};

export const crearSucursal = async (payload) => {
    const res = await API.post("/admin/sucursales", payload);
    return res.data;
};

export const actualizarSucursal = async (id, payload) => {
    const res = await API.put(`/admin/sucursales/${id}`, payload);
    return res.data;
};

export const eliminarSucursalService = async (id) => {
    const res = await API.delete(`/admin/sucursales/${id}`);
    return res.data;
};