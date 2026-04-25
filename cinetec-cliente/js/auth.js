/**
 * Funciones de autenticación para CineTec Cliente
 */

/**
 * Iniciar sesión del usuario (verificar cédula)
 * @param {string} cedula - Cédula del usuario
 * @returns {Promise} Datos del usuario si existe
 */
async function iniciarSesion(cedula) {
  try {
    controlarLoading('loading', true);
    
    // Validar que la cédula no esté vacía
    if (!cedula || cedula.trim() === '') {
      throw new Error('Por favor ingresa tu cédula');
    }

    // Hacer petición al API
    const resultado = await apiPost('usuarios/login', { cedula: cedula.trim() });

    // Guardar usuario en localStorage
    guardarUsuarioActual(resultado.usuario);

    return resultado.usuario;
  } catch (error) {
    console.error('Error al iniciar sesión:', error);
    throw error;
  } finally {
    controlarLoading('loading', false);
  }
}

/**
 * Registrar un nuevo usuario
 * @param {object} datosUsuario - Objeto con datos del usuario
 * @returns {Promise} Datos del usuario registrado
 */
async function registrarUsuario(datosUsuario) {
  try {
    controlarLoading('loading', true);

    // Validar que todos los campos requeridos estén presentes
    const { nombre, apellido, cedula, telefono, fechaNacimiento } = datosUsuario;

    if (!nombre || !apellido || !cedula || !fechaNacimiento) {
      throw new Error('Por favor completa todos los campos requeridos');
    }

    // Validar formato de cédula
    if (!validarCedula(cedula)) {
      throw new Error('La cédula debe ser válida (ej: 123456789)');
    }

    // Validar teléfono si se proporciona
    if (telefono && !validarTelefono(telefono)) {
      throw new Error('El teléfono debe tener 8 dígitos');
    }

    // Validar fecha de nacimiento
    const edad = calcularEdad(fechaNacimiento);
    if (edad < 13) {
      throw new Error('Debes tener al menos 13 años para registrarte');
    }

    // Preparar datos para enviar
    const datosRegistro = {
      nombre: nombre.trim(),
      apellido: apellido.trim(),
      cedula: cedula.trim(),
      telefono: telefono ? telefono.trim() : '',
      fechaNacimiento: fechaNacimiento,
    };

    // Hacer petición al API
    const resultado = await apiPost('usuarios/registrar', datosRegistro);

    // Guardar usuario en localStorage
    guardarUsuarioActual(resultado.usuario);

    return resultado.usuario;
  } catch (error) {
    console.error('Error al registrar usuario:', error);
    throw error;
  } finally {
    controlarLoading('loading', false);
  }
}

/**
 * Actualizar datos del usuario actual
 * @param {object} datosActualizados - Datos a actualizar
 * @returns {Promise} Usuario actualizado
 */
async function actualizarUsuario(datosActualizados) {
  try {
    const usuario = obtenerUsuarioActual();
    if (!usuario) {
      throw new Error('No hay usuario en sesión');
    }

    controlarLoading('loading', true);

    const resultado = await apiPut(`usuarios/${usuario.id}`, datosActualizados);

    // Actualizar usuario en localStorage
    guardarUsuarioActual(resultado.usuario);

    return resultado.usuario;
  } catch (error) {
    console.error('Error al actualizar usuario:', error);
    throw error;
  } finally {
    controlarLoading('loading', false);
  }
}

/**
 * Verificar si hay usuario en sesión
 * @returns {boolean} True si hay usuario, False si no
 */
function verificarSesion() {
  return obtenerUsuarioActual() !== null;
}

/**
 * Verificar si el usuario está autenticado, si no, redirigir a login
 */
function protegerRuta() {
  if (!verificarSesion()) {
    redirigir('index.html', 500);
  }
}

/**
 * Cerrar la sesión del usuario
 */
function salir() {
  cerrarSesion();
  mostrarExito('Sesión cerrada correctamente');
  redirigir('index.html', 1500);
}
