import { useNavigate } from 'react-router-dom'
import { useState } from 'react'

function Login() {
    const navigate = useNavigate()
    const [usuario, setUsuario] = useState('')
    const [contrasena, setContrasena] = useState('')

    const manejarLogin = (e) => {
        e.preventDefault()

        if (usuario === 'admin' && contrasena === '1234') {
            navigate('/admin')
        } else {
            alert('Credenciales incorrectas')
        }
    }

    return (
        <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
            <div className="card shadow p-4" style={{ width: '400px' }}>
                <h2 className="text-center mb-4">CineTEC</h2>

                <form onSubmit={manejarLogin}>
                    <input
                        className="form-control mb-3"
                        placeholder="Usuario"
                        onChange={(e) => setUsuario(e.target.value)}
                    />

                    <input
                        type="password"
                        className="form-control mb-3"
                        placeholder="Contraseña"
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