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
import { login } from '../services/authService' // IMPORTANTE

function Login() {

    const navigate = useNavigate()

    const [usuario, setUsuario] = useState('')
    const [contrasena, setContrasena] = useState('')

    //LOGIN REAL
    const manejarLogin = async (e) => {
        e.preventDefault();

        if (!usuario.trim() || !contrasena.trim()) {
            alert("Debes ingresar usuario y contraseña");
            return;
        }

        try {
            const data = await login(usuario, contrasena);

            localStorage.setItem("usuario", JSON.stringify(data));
            navigate("/admin");

        } catch (error) {
            console.error("Error en login:", error);

            let mensaje = "Ocurrió un error al iniciar sesión";

            if (error.response) {
                if (error.response.status === 401) {
                    mensaje = "Usuario o contraseña incorrectos";
                } else if (error.response.status === 404) {
                    mensaje = "No se encontró el servicio de login";
                } else if (error.response.status === 500) {
                    mensaje = "Error interno del servidor";
                } else {
                    mensaje =
                        error.response.data?.message ||
                        error.response.data?.error ||
                        "Error al iniciar sesión";
                }
            } else if (error.request) {
                mensaje = "No se pudo conectar con el servidor";
            } else {
                mensaje = error.message || "Error inesperado";
            }

            alert(mensaje);
        }
    };

    return (
        <div className="d-flex justify-content-center align-items-center vh-100 bg-light">

            <div className="card shadow p-4" style={{ width: '400px' }}>

                <h2 className="text-center mb-4">CineTEC</h2>

                <form onSubmit={manejarLogin}>

                    <input
                        className="form-control mb-3"
                        placeholder="Usuario"
                        value={usuario}
                        onChange={(e) => setUsuario(e.target.value)}
                    />

                    <input
                        type="password"
                        className="form-control mb-3"
                        placeholder="Contraseña"
                        value={contrasena}
                        onChange={(e) => setContrasena(e.target.value)}
                    />

                    <button className="btn btn-dark w-100">
                        Ingresar
                    </button>
                </form>
            </div>
        </div>
    )
}

export default Login