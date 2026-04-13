import { useEffect, useState } from "react";
import API from "../services/api";

function Peliculas() {
    const [peliculas, setPeliculas] = useState([]);

    useEffect(() => {
        obtenerPeliculas();
    }, []);

    const obtenerPeliculas = async () => {
        try {
            const res = await API.get("/admin/peliculas");
            setPeliculas(res.data);
        } catch (error) {
            console.error(error);
        }
    };

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
                        {peliculas.map((p) => (
                            <tr key={p.id}>
                                <td>{p.id}</td>
                                <td>{p.nombreComercial}</td>
                                <td>{p.duracion}</td>
                                <td>{p.director}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default Peliculas;