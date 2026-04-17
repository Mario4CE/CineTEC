import API from "./api";

export const login = async (usuario, contrasena) => {
    const res = await API.post("/auth/login", {
        usuario,
        contrasena
    });

    return res.data;
};