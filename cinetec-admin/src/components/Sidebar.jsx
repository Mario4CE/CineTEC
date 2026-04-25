/*
DESCRIPCIÓN:
Este componente representa el menú lateral (Sidebar) del panel administrativo.
Permite la navegación entre las diferentes secciones del sistema como
Dashboard, Películas, Sucursales, Salas y Proyecciones.

ENTRADAS:
- No recibe entradas directas (no props).
- Interacción del usuario mediante clics en los enlaces.

SALIDAS:
- Redirección a las distintas rutas del sistema.
- Renderización del menú lateral en la interfaz.

RESTRICCIONES:
- Depende de react-router-dom para la navegación.
- Las rutas deben existir en el sistema (definidas en App.jsx).
- Debe estar contenido dentro de un Layout para funcionar correctamente.

Modificado por: Mario
*/

import { Link } from 'react-router-dom'

function Sidebar() {
    return (
        // Contenedor del sidebar
        <div className="bg-dark text-white p-3 vh-100" style={{ width: '250px' }}>

            {/* Título */}
            <h3 className="mb-4">CineTEC Admin</h3>

            {/* Menú de navegación */}
            <ul className="nav flex-column">

                {/* Dashboard */}
                <li className="nav-item mb-2">
                    <Link to="/admin" className="nav-link text-white">
                        Dashboard
                    </Link>
                </li>

                {/* Películas */}
                <li className="nav-item mb-2">
                    <Link to="/admin/peliculas" className="nav-link text-white">
                        Películas
                    </Link>
                </li>

                {/* Sucursales */}
                <li className="nav-item mb-2">
                    <Link to="/admin/sucursales" className="nav-link text-white">
                        Sucursales
                    </Link>
                </li>

                {/* Salas */}
                <li className="nav-item mb-2">
                    <Link to="/admin/salas" className="nav-link text-white">
                        Salas
                    </Link>
                </li>

                {/* Proyecciones */}
                <li className="nav-item mb-2">
                    <Link to="/admin/proyecciones" className="nav-link text-white">
                        Proyecciones
                    </Link>
                </li>
                
                {/* Clientes */}
                <li className="nav-item mb-2">
                    <Link to="/admin/clientes" className="nav-link text-white">
                        Clientes
                    </Link>
                </li>

                {/* Restricciones de Asientos */}
                <li className="nav-item mb-2">
                    <Link to="/admin/restricciones-asientos" className="nav-link text-white">
                        Restricciones de Asientos
                    </Link>
                </li>

            </ul>
        </div>
    )
}

export default Sidebar