import { useEffect, useState } from "react";
import API from "../services/api";

function Peliculas() {
    const [peliculas, setPeliculas] = useState([]);
    const [cargando, setCargando] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const cargarPeliculas = async () => {
            try {
                const res = await API.get("/admin/peliculas");
                console.log("Respuesta API:", res.data);
                setPeliculas(res.data);
            } catch (err) {
                console.error("Error al obtener películas:", err);
                setError("No se pudieron cargar las películas.");
            } finally {
                setCargando(false);
            }
        };

        cargarPeliculas();
    }, []);

    if (cargando) {
        return <h3 className="p-4">Cargando películas...</h3>;
    }

    if (error) {
        return <h3 className="p-4 text-danger">{error}</h3>;
    }

    return (
        <div>
            <h2 className="mb-4">Películas</h2>

            <div className="card shadow-sm p-3">
                <table className="table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre</th>
                            <th>Duración</th>
                            <th>Director</th>
                        </tr>
                    </thead>

                    <tbody>
                        {peliculas.length > 0 ? (
                            peliculas.map((p) => (
                                <tr key={p.id}>
                                    <td>{p.id}</td>
                                    <td>{p.nombreComercial}</td>
                                    <td>{p.duracion}</td>
                                    <td>{p.director}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="4" className="text-center">
                                    No hay películas registradas.
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default Peliculas;