/**
 * Configuración de la API de CineTec
 * Este archivo contiene las funciones para comunicarse con el backend C#
 */

const API_BASE_URL = 'http://localhost:5000/api';

/**
 * Realizar una solicitud GET a la API
 * @param {string} endpoint - Ruta del endpoint (ej: 'usuarios/cedula/123456')
 * @returns {Promise} Respuesta de la API
 */
async function apiGet(endpoint) {
  try {
    const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
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
    const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
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
    const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
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
    const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
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
