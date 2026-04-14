import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Login from './pages/Login'
import Dashboard from './pages/Dashboard'
import Peliculas from './pages/Peliculas'
import Sucursales from './pages/Sucursales'
import Salas from './pages/Salas'
import Proyecciones from './pages/Proyecciones'
import Layout from './components/Layout'

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Login />} />

                <Route path="/admin" element={<Layout />}>
                    <Route index element={<Dashboard />} />
                    <Route path="peliculas" element={<Peliculas />} />
                    <Route path="sucursales" element={<Sucursales />} />
                    <Route path="salas" element={<Salas />} />
                    <Route path="proyecciones" element={<Proyecciones />} />
                </Route>
            </Routes>
        </BrowserRouter>
    )
}

export default App