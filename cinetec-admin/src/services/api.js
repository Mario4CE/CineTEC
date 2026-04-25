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
- Puede configurarse con la variable de entorno VITE_API_URL.
- También permite sobreescribir la URL por window.CINETEC_API_URL o localStorage.
- Si no existe configuración, usa "http://<host-actual>:5000/api".
- Depende de la librería axios.
*/

import axios from "axios";

function normalizarUrl(url) {
    return url.replace(/\/$/, "");
}

function ajustarUrlParaAccesoRemoto(url) {
    try {
        const hostActual = window.location.hostname || "";
        const hostEsRemoto = !["localhost", "127.0.0.1", "::1"].includes(hostActual);
        const urlParseada = new URL(url);
        const urlEsLocal = ["localhost", "127.0.0.1", "::1"].includes(urlParseada.hostname);

        if (hostEsRemoto && urlEsLocal) {
            urlParseada.hostname = hostActual;
        }

        return normalizarUrl(urlParseada.toString());
    } catch {
        return normalizarUrl(url);
    }
}

function resolverApiBaseUrl() {
    const envUrl = import.meta.env.VITE_API_URL;
    const globalUrl = window.CINETEC_API_URL;
    const storageUrl = localStorage.getItem("cinetec_api_url");

    if (typeof envUrl === "string" && envUrl.trim() !== "") {
        return ajustarUrlParaAccesoRemoto(envUrl);
    }

    if (typeof globalUrl === "string" && globalUrl.trim() !== "") {
        return ajustarUrlParaAccesoRemoto(globalUrl);
    }

    if (typeof storageUrl === "string" && storageUrl.trim() !== "") {
        return ajustarUrlParaAccesoRemoto(storageUrl);
    }

    const host = window.location.hostname || "localhost";
    return `http://${host}:5000/api`;
}

const API = axios.create({
    baseURL: resolverApiBaseUrl()
});

API.interceptors.request.use((config) => {
    if (
        typeof config.url === "string" &&
        !/^https?:\/\//i.test(config.url)
    ) {
        config.url = config.url.replace(/^\/+/, "");
    }

    return config;
});

export default API;