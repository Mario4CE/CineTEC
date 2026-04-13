import { Link } from 'react-router-dom'

function Sidebar() {
    return (
        <div className="bg-dark text-white p-3 vh-100" style={{ width: '250px' }}>
            <h3 className="mb-4">CineTEC Admin</h3>

            <ul className="nav flex-column">
                <li className="nav-item mb-2">
                    <Link to="/admin" className="nav-link text-white">Dashboard</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link to="/admin/peliculas" className="nav-link text-white">Películas</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link to="/admin/sucursales" className="nav-link text-white">Sucursales</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link to="/admin/salas" className="nav-link text-white">Salas</Link>
                </li>
                <li className="nav-item mb-2">
                    <Link to="/admin/proyecciones" className="nav-link text-white">Proyecciones</Link>
                </li>
            </ul>
        </div>
    )
}

export default Sidebar