/**
 * Funciones utilitarias para CineTec Cliente
 */

/**
 * Mostrar un mensaje de error
 * @param {string} mensaje - Mensaje de error
 * @param {string} elementoId - ID del elemento donde mostrar el error (opcional)
 */
function mostrarError(mensaje, elementoId = 'alertError') {
  const elemento = document.getElementById(elementoId);
  if (elemento) {
    elemento.textContent = mensaje;
    elemento.style.display = 'block';
    elemento.classList.add('show');
    
    // Auto-ocultar después de 5 segundos
    setTimeout(() => {
      elemento.style.display = 'none';
      elemento.classList.remove('show');
    }, 5000);
  }
}

/**
 * Mostrar un mensaje de éxito
 * @param {string} mensaje - Mensaje de éxito
 * @param {string} elementoId - ID del elemento donde mostrar el éxito (opcional)
 */
function mostrarExito(mensaje, elementoId = 'alertSuccess') {
  const elemento = document.getElementById(elementoId);
  if (elemento) {
    elemento.textContent = mensaje;
    elemento.style.display = 'block';
    elemento.classList.add('show');
    
    // Auto-ocultar después de 3 segundos
    setTimeout(() => {
      elemento.style.display = 'none';
      elemento.classList.remove('show');
    }, 3000);
  }
}

/**
 * Validar formato de cédula (Costa Rica)
 * @param {string} cedula - Cédula a validar
 * @returns {boolean} True si es válida
 */
function validarCedula(cedula) {
  // Aceptar: 123456789, 1-2345-6789, 12345678901
  const regexCedula = /^(\d{9}|\d{1}-\d{4}-\d{4}|\d{11})$/;
  return regexCedula.test(cedula);
}

/**
 * Validar email (formato básico)
 * @param {string} email - Email a validar
 * @returns {boolean} True si es válido
 */
function validarEmail(email) {
  const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regexEmail.test(email);
}

/**
 * Validar teléfono (Costa Rica - 8 dígitos)
 * @param {string} telefono - Teléfono a validar
 * @returns {boolean} True si es válido
 */
function validarTelefono(telefono) {
  const regexTelefono = /^\d{8}$/;
  return regexTelefono.test(telefono);
}

/**
 * Calcular edad a partir de fecha de nacimiento
 * @param {string} fechaNacimiento - Fecha en formato YYYY-MM-DD
 * @returns {number} Edad en años
 */
function calcularEdad(fechaNacimiento) {
  const hoy = new Date();
  let edad = hoy.getFullYear() - new Date(fechaNacimiento).getFullYear();
  const mes = hoy.getMonth() - new Date(fechaNacimiento).getMonth();
  
  if (mes < 0 || (mes === 0 && hoy.getDate() < new Date(fechaNacimiento).getDate())) {
    edad--;
  }
  
  return edad;
}

/**
 * Guardar datos en localStorage
 * @param {string} clave - Clave para guardar
 * @param {any} valor - Valor a guardar (se convierte a JSON)
 */
function guardarEnLocal(clave, valor) {
  localStorage.setItem(clave, JSON.stringify(valor));
}

/**
 * Obtener datos de localStorage
 * @param {string} clave - Clave a obtener
 * @returns {any} Valor guardado (parseado de JSON)
 */
function obtenerDeLocal(clave) {
  const valor = localStorage.getItem(clave);
  return valor ? JSON.parse(valor) : null;
}

/**
 * Limpiar localStorage
 * @param {string} clave - Clave a eliminar (opcional, si no se especifica, limpia todo)
 */
function limpiarLocal(clave) {
  if (clave) {
    localStorage.removeItem(clave);
  } else {
    localStorage.clear();
  }
}

/**
 * Desabilitar/Habilitar un botón
 * @param {string} botonId - ID del botón
 * @param {boolean} desabilitar - True para desabilitar, False para habilitar
 */
function controlarBoton(botonId, desabilitar) {
  const boton = document.getElementById(botonId);
  if (boton) {
    boton.disabled = desabilitar;
    boton.style.opacity = desabilitar ? '0.6' : '1';
  }
}

/**
 * Mostrar loading
 * @param {string} elementoId - ID del elemento loading
 * @param {boolean} mostrar - True para mostrar, False para ocultar
 */
function controlarLoading(elementoId, mostrar) {
  const elemento = document.getElementById(elementoId);
  if (elemento) {
    elemento.style.display = mostrar ? 'block' : 'none';
  }
}

/**
 * Redirigir a otra página
 * @param {string} ruta - Ruta a la que redirigir
 * @param {number} tiempo - Tiempo de espera antes de redirigir (ms)
 */
function redirigir(ruta, tiempo = 0) {
  if (tiempo > 0) {
    setTimeout(() => {
      window.location.href = ruta;
    }, tiempo);
  } else {
    window.location.href = ruta;
  }
}

/**
 * Obtener usuario de la sesión
 * @returns {object} Datos del usuario o null
 */
function obtenerUsuarioActual() {
  return obtenerDeLocal('usuarioActual');
}

/**
 * Guardar usuario en la sesión
 * @param {object} usuario - Datos del usuario
 */
function guardarUsuarioActual(usuario) {
  guardarEnLocal('usuarioActual', usuario);
}

/**
 * Cerrar sesión del usuario
 */
function cerrarSesion() {
  limpiarLocal('usuarioActual');
  redirigir('index.html');
}

/**
 * Verificar si hay una sesión activa
 * @returns {boolean} True si hay sesión activa
 */
function verificarSesion() {
  return obtenerUsuarioActual() !== null;
}

/**
 * Proteger una ruta requiriendo autenticación
 * Si no hay sesión activa, redirige a login
 */
function protegerRuta() {
  if (!verificarSesion()) {
    redirigir('index.html');
  }
}
