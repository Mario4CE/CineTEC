/*
DESCRIPCIÓN:
Este archivo define la configuración principal de rutas de la aplicación web.
Se encarga de dirigir al usuario a las diferentes páginas (Login, Dashboard,
Películas, Sucursales, Salas y Proyecciones) utilizando React Router.

ENTRADAS:
- URL ingresada por el usuario en el navegador.
- Interacciones del usuario que cambian la ruta (navegación).

SALIDAS:
- Renderización del componente correspondiente según la ruta.
- Visualización de la interfaz adecuada (login o panel administrativo).

RESTRICCIONES:
- Se debe utilizar dentro de un entorno React.
- Requiere que las rutas y componentes estén correctamente importados.
- Depende de la librería react-router-dom para funcionar.
- La ruta "/admin" usa un Layout, por lo que sus rutas hijas se renderizan dentro de él.
Modificado por: Mario 
*/

import { BrowserRouter, Routes, Route } from 'react-router-dom'

// Importación de páginas principales
import Login from './pages/Login'
import Dashboard from './pages/Dashboard'
import Peliculas from './pages/Peliculas'
import Sucursales from './pages/Sucursales'
import Salas from './pages/Salas'
import Proyecciones from './pages/Proyecciones'
import Clientes from './pages/Clientes'
import RestriccionesAsientos from './pages/RestriccionesAsientos'

// Componente de estructura base (layout del admin)
import Layout from './components/Layout'

function App() {
    return (
        // BrowserRouter permite manejar la navegación en la aplicación
        <BrowserRouter>

            {/* Contenedor de todas las rutas */}
            <Routes>

                {/* Ruta principal: muestra el login */}
                <Route path="/" element={<Login />} />

                {/* Ruta padre del panel administrativo */}
                <Route path="/admin" element={<Layout />}>

                    {/* Ruta por defecto dentro de /admin */}
                    <Route index element={<Dashboard />} />

                    {/* Subruta para gestión de películas */}
                    <Route path="peliculas" element={<Peliculas />} />

                    {/* Subruta para gestión de sucursales */}
                    <Route path="sucursales" element={<Sucursales />} />

                    {/* Subruta para gestión de salas */}
                    <Route path="salas" element={<Salas />} />

                    {/* Subruta para gestión de proyecciones */}
                    <Route path="proyecciones" element={<Proyecciones />} />

                    {/* Subruta para gestión de clientes */}
                    <Route path="clientes" element={<Clientes />} />

                    {/* Subruta para gestión de restricciones de asientos */}
                    <Route path="restricciones-asientos" element={<RestriccionesAsientos />}/>

                </Route>
            </Routes>
        </BrowserRouter>
    )
}

// Exportación del componente principal
export default App