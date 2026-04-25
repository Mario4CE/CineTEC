/**
 * Configuración de la API de CineTec
 * Este archivo contiene las funciones para comunicarse con el backend C#
 */

function normalizarUrl(url) {
    return url.replace(/\/$/, '');
}

function esHostLocal(hostname) {
    return ['localhost', '127.0.0.1', '::1'].includes(hostname); // Agrega más hosts locales si es necesario
}

function ajustarUrlParaAccesoRemoto(url) {
    try {
        const hostActual = window.location.hostname || '';
        const hostEsRemoto = !esHostLocal(hostActual);
        const urlParseada = new URL(url);

        if (hostEsRemoto && esHostLocal(urlParseada.hostname)) {
            urlParseada.hostname = hostActual;
        }

        return normalizarUrl(urlParseada.toString());
    } catch (error) {
        console.warn('No se pudo parsear la URL de API, se usará tal cual:', error);
        return normalizarUrl(url);
    }
}

function resolverApiBaseUrl() {
    const globalUrl = window.CINETEC_API_URL;
    const storageUrl = localStorage.getItem('cinetec_api_url');

    if (typeof globalUrl === 'string' && globalUrl.trim() !== '') {
        return ajustarUrlParaAccesoRemoto(globalUrl);
    }

    if (typeof storageUrl === 'string' && storageUrl.trim() !== '') {
        return ajustarUrlParaAccesoRemoto(storageUrl);
    }

    const host = window.location.hostname || 'localhost';
    return `http://${host}:5000/api`; // URL por defecto si no se encuentra ninguna configuración
}

const API_BASE_URL = resolverApiBaseUrl();

function construirUrl(endpoint) {
    const endpointNormalizado = String(endpoint || '').replace(/^\/+/, '');
    return `${API_BASE_URL}/${endpointNormalizado}`;
}

/**
 * Realizar una solicitud GET a la API
 * @param {string} endpoint - Ruta del endpoint (ej: 'usuarios/cedula/123456')
 * @returns {Promise} Respuesta de la API
 */
async function apiGet(endpoint) {
    try {
        const response = await fetch(construirUrl(endpoint), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.mensaje || `Error HTTP: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Error en GET:', error);
        throw error;
    }
}

/**
 * Realizar una solicitud POST a la API
 * @param {string} endpoint - Ruta del endpoint (ej: 'usuarios/registrar')
 * @param {object} data - Datos a enviar
 * @returns {Promise} Respuesta de la API
 */
async function apiPost(endpoint, data) {
    try {
        const response = await fetch(construirUrl(endpoint), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.mensaje || `Error HTTP: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Error en POST:', error);
        throw error;
    }
}

/**
 * Realizar una solicitud PUT a la API
 * @param {string} endpoint - Ruta del endpoint (ej: 'usuarios/1')
 * @param {object} data - Datos a actualizar
 * @returns {Promise} Respuesta de la API
 */
async function apiPut(endpoint, data) {
    try {
        const response = await fetch(construirUrl(endpoint), {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.mensaje || `Error HTTP: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Error en PUT:', error);
        throw error;
    }
}

/**
 * Realizar una solicitud DELETE a la API
 * @param {string} endpoint - Ruta del endpoint (ej: 'usuarios/1')
 * @returns {Promise} Respuesta de la API
 */
async function apiDelete(endpoint) {
    try {
        const response = await fetch(construirUrl(endpoint), {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.mensaje || `Error HTTP: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Error en DELETE:', error);
        throw error;
    }
}
