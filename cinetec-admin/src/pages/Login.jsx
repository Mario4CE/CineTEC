/*
DESCRIPCIÓN:
Este componente representa la pantalla de inicio de sesión del sistema.
Permite al usuario ingresar sus credenciales y autenticarse contra el backend.
Si el inicio de sesión es exitoso, redirige al panel administrativo.

ENTRADAS:
- Usuario (texto ingresado en el input).
- Contraseña (texto ingresado en el input).
- Evento de envío del formulario.

SALIDAS:
- Redirección a la ruta "/admin" si las credenciales son válidas.
- Mensaje de error (alert) si ocurre un problema en la autenticación.

RESTRICCIONES:
- Depende del servicio login() definido en authService.
- Requiere conexión con el backend para validar credenciales.
- Depende de react-router-dom para la navegación.
*/

import { useNavigate } from 'react-router-dom'
import { useState } from 'react'
import { login } from '../services/authService'

function Login() {
    const navigate = useNavigate()

    const [usuario, setUsuario] = useState('')
    const [contrasena, setContrasena] = useState('')

    const manejarLogin = async (e) => {
        e.preventDefault()

        if (!usuario.trim() || !contrasena.trim()) {
            alert("Debes ingresar usuario y contraseña")
            return
        }

        try {
            const data = await login(usuario, contrasena)

            localStorage.setItem("usuario", JSON.stringify(data))
            navigate("/admin")
        } catch (error) {
            console.error("Error en login:", error)

            let mensaje = "Ocurrió un error al iniciar sesión"

            if (error.response) {
                if (error.response.status === 401) {
                    mensaje = "Usuario o contraseña incorrectos"
                } else if (error.response.status === 404) {
                    mensaje = "No se encontró el servicio de login"
                } else if (error.response.status === 500) {
                    mensaje = "Error interno del servidor"
                } else {
                    mensaje =
                        error.response.data?.message ||
                        error.response.data?.error ||
                        "Error al iniciar sesión"
                }
            } else if (error.request) {
                mensaje = "No se pudo conectar con el servidor"
            } else {
                mensaje = error.message || "Error inesperado"
            }

            alert(mensaje)
        }
    }

    return (
        <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
            <div className="card shadow p-4" style={{ width: '400px' }}>
                <h2 className="text-center mb-4">CineTEC</h2>

                <form onSubmit={manejarLogin}>
                    <input
                        type="text"
                        className="form-control mb-3"
                        placeholder="Usuario"
                        value={usuario}
                        onChange={(e) => setUsuario(e.target.value)}
                        autoComplete="username"
                        required
                    />

                    <input
                        type="password"
                        className="form-control mb-3"
                        placeholder="Contraseña"
                        value={contrasena}
                        onChange={(e) => setContrasena(e.target.value)}
                        autoComplete="current-password"
                        required
                    />

                    <button type="submit" className="btn btn-dark w-100">
                        Ingresar
                    </button>
                </form>
            </div>
        </div>
    )
}

export default Login