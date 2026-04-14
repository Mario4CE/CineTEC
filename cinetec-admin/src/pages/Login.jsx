/*
DESCRIPCIÓN:
Este componente representa la pantalla de inicio de sesión del sistema.
Permite al usuario ingresar sus credenciales y, si son correctas,
redirige al panel administrativo.

ENTRADAS:
- Usuario (texto ingresado en el input).
- Contraseña (texto ingresado en el input).
- Evento de envío del formulario.

SALIDAS:
- Redirección a la ruta "/admin" si las credenciales son correctas.
- Mensaje de error (alert) si las credenciales son incorrectas.

RESTRICCIONES:
- Las credenciales están definidas de forma estática (usuario: admin, contraseña: 1234).
- No hay conexión con base de datos ni autenticación real.
- Depende de react-router-dom para la navegación.

Modificado por: Mario 
*/

import { useNavigate } from 'react-router-dom'
import { useState } from 'react'

function Login() {

    // Hook para navegar entre rutas
    const navigate = useNavigate()

    // Estados para almacenar usuario y contraseña
    const [usuario, setUsuario] = useState('')
    const [contrasena, setContrasena] = useState('')

    // Función que maneja el login
    const manejarLogin = (e) => {
        e.preventDefault() // Evita recargar la página

        // Validación simple de credenciales
        if (usuario === 'admin' && contrasena === '1234') {
            // Redirige al panel admin
            navigate('/admin')
        } else {
            // Muestra error
            alert('Credenciales incorrectas')
        }
    }

    return (
        // Contenedor principal centrado
        <div className="d-flex justify-content-center align-items-center vh-100 bg-light">

            {/* Tarjeta de login */}
            <div className="card shadow p-4" style={{ width: '400px' }}>

                <h2 className="text-center mb-4">CineTEC</h2>

                {/* Formulario */}
                <form onSubmit={manejarLogin}>

                    {/* Input usuario */}
                    <input
                        className="form-control mb-3"
                        placeholder="Usuario"
                        onChange={(e) => setUsuario(e.target.value)}
                    />

                    {/* Input contraseña */}
                    <input
                        type="password"
                        className="form-control mb-3"
                        placeholder="Contraseña"
                        onChange={(e) => setContrasena(e.target.value)}
                    />

                    {/* Botón enviar */}
                    <button className="btn btn-dark w-100">
                        Ingresar
                    </button>
                </form>
            </div>
        </div>
    )
}

export default Login