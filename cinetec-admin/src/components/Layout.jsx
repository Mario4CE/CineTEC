/*
DESCRIPCIÓN:
Este componente define la estructura principal del panel administrativo.
Organiza la interfaz en tres partes: Sidebar (menú lateral),
Navbar (barra superior) y el contenido dinámico (Outlet).
Permite reutilizar la misma estructura para todas las páginas del sistema.

ENTRADAS:
- Componentes hijos que se renderizan dinámicamente mediante <Outlet />.
- Rutas definidas en el sistema (react-router-dom).

SALIDAS:
- Renderización de la estructura completa del panel administrativo.
- Visualización del contenido correspondiente según la ruta activa.

RESTRICCIONES:
- Depende de react-router-dom para el uso de <Outlet />.
- Requiere que Sidebar y Navbar estén correctamente importados.
- Solo funciona correctamente dentro de rutas anidadas (ej: /admin).

Modificado por: Mario
*/

import { Outlet } from 'react-router-dom'
import Sidebar from './Sidebar'
import Navbar from './Navbar'

function Layout() {
    return (
        // Contenedor principal con flexbox
        <div className="d-flex">

            {/* Menú lateral */}
            <Sidebar />

            {/* Contenedor principal de contenido */}
            <div className="flex-grow-1">

                {/* Barra superior */}
                <Navbar />

                {/* Contenido dinámico según la ruta */}
                <div className="container-fluid p-4">
                    <Outlet />
                </div>

            </div>
        </div>
    )
}

export default Layout