
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
 - La URL base debe coincidir con la del backend en ejecución.
-- Requiere que el backend esté activo en "http://localhost:5000/api".
+- Puede configurarse con la variable de entorno VITE_API_URL.
+- Si no existe VITE_API_URL, usa "http://localhost:5000/api".
 - Depende de la librería axios.
 
 */

function resolverApiBaseUrl() {
  const urlGlobal = window.CINETEC_API_URL;
  const urlStorage = localStorage.getItem("cinetec_api_url");

  if (urlGlobal && typeof urlGlobal === "string") {
    return urlGlobal.replace(/\/$/, "");
  }

  if (urlStorage && typeof urlStorage === "string") {
    return urlStorage.replace(/\/$/, "");
  }

  const host = window.location.hostname || "localhost";
  return `http://${host}:5000/api`;
}

const API_BASE_URL = resolverApiBaseUrl();

/**
 * Convierte la respuesta de error en un mensaje legible
 * @param {Response} response
 * @returns {Promise<string>}
 */
async function leerError(response) {
  try {
    const contentType = response.headers.get("content-type") || "";

    if (contentType.includes("application/json")) {
      const errorData = await response.json();

      return (
        errorData?.mensaje ||
        errorData?.message ||
        errorData?.error ||
        `Error HTTP: ${response.status}`
      );
    }

    const texto = await response.text();
    return texto || `Error HTTP: ${response.status}`;
  } catch {
    return `Error HTTP: ${response.status}`;
  }
}

/**
 * Intenta leer la respuesta como JSON.
 * Si no trae contenido, devuelve null.
 * @param {Response} response
 * @returns {Promise<any>}
 */
async function leerRespuesta(response) {
  if (response.status === 204) {
    return null;
  }

  const contentType = response.headers.get("content-type") || "";

  if (contentType.includes("application/json")) {
    return await response.json();
  }

  return await response.text();
}

/**
 * Función base para realizar solicitudes HTTP
 * @param {string} endpoint
 * @param {string} method
 * @param {object|null} data
 * @returns {Promise<any>}
 */
async function apiRequest(endpoint, method = "GET", data = null) {
  try {
    const options = {
      method,
      headers: {
        "Content-Type": "application/json",
      },
    };

    if (data !== null) {
      options.body = JSON.stringify(data);
    }

    const response = await fetch(`${API_BASE_URL}/${endpoint}`, options);

    if (!response.ok) {
      const mensajeError = await leerError(response);
      throw new Error(mensajeError);
    }

    return await leerRespuesta(response);
  } catch (error) {
    console.error(`Error en ${method}:`, error);
    throw error;
  }
}

/**
 * Realizar una solicitud GET a la API
 * @param {string} endpoint - Ruta del endpoint
 * @returns {Promise<any>}
 */
async function apiGet(endpoint) {
  return await apiRequest(endpoint, "GET");
}

/**
 * Realizar una solicitud POST a la API
 * @param {string} endpoint - Ruta del endpoint
 * @param {object} data - Datos a enviar
 * @returns {Promise<any>}
 */
async function apiPost(endpoint, data) {
  return await apiRequest(endpoint, "POST", data);
}

/**
 * Realizar una solicitud PUT a la API
 * @param {string} endpoint - Ruta del endpoint
 * @param {object} data - Datos a actualizar
 * @returns {Promise<any>}
 */
async function apiPut(endpoint, data) {
  return await apiRequest(endpoint, "PUT", data);
}

/**
 * Realizar una solicitud DELETE a la API
 * @param {string} endpoint - Ruta del endpoint
 * @returns {Promise<any>}
 */
async function apiDelete(endpoint) {
  return await apiRequest(endpoint, "DELETE");
}