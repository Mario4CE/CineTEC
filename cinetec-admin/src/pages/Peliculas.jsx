import { useEffect, useState } from "react";
import API from "../services/api";

function Peliculas() {
    const [peliculas, setPeliculas] = useState([]);
    const [cargando, setCargando] = useState(true);
    const [error, setError] = useState("");

    const [modoEdicion, setModoEdicion] = useState(false);
    const [idEditar, setIdEditar] = useState(null);

    const [form, setForm] = useState({
        nombreOriginal: "",
        nombreComercial: "",
        imagen: "",
        duracion: "",
        protagonistas: "",
        director: "",
        clasificacion: ""
    });

    useEffect(() => {
        cargarPeliculas();
    }, []);

    const cargarPeliculas = async () => {
        try {
            setCargando(true);
            const res = await API.get("/admin/peliculas");
            setPeliculas(res.data);
            setError("");
        } catch (err) {
            console.error("Error al obtener películas:", err);
            setError("No se pudieron cargar las películas.");
        } finally {
            setCargando(false);
        }
    };

    const manejarCambio = (e) => {
        const { name, value } = e.target;
        setForm({
            ...form,
            [name]: value
        });
    };

    const limpiarFormulario = () => {
        setForm({
            nombreOriginal: "",
            nombreComercial: "",
            imagen: "",
            duracion: "",
            protagonistas: "",
            director: "",
            clasificacion: ""
        });
        setModoEdicion(false);
        setIdEditar(null);
    };

    const manejarSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            ...form,
            duracion: Number(form.duracion)
        };

        try {
            if (modoEdicion) {
                await API.put(`/admin/peliculas/${idEditar}`, payload);
                alert("Película actualizada correctamente");
            } else {
                await API.post("/admin/peliculas", payload);
                alert("Película agregada correctamente");
            }

            limpiarFormulario();
            cargarPeliculas();
        } catch (err) {
            console.error("Error al guardar película:", err);
            alert("Ocurrió un error al guardar la película");
        }
    };

    const editarPelicula = (pelicula) => {
        setModoEdicion(true);
        setIdEditar(pelicula.id);

        setForm({
            nombreOriginal: pelicula.nombreOriginal || "",
            nombreComercial: pelicula.nombreComercial || "",
            imagen: pelicula.imagen || "",
            duracion: pelicula.duracion || "",
            protagonistas: pelicula.protagonistas || "",
            director: pelicula.director || "",
            clasificacion: pelicula.clasificacion || ""
        });
    };

    const eliminarPelicula = async (id) => {
        const confirmar = window.confirm("¿Desea eliminar esta película?");
        if (!confirmar) return;

        try {
            await API.delete(`/admin/peliculas/${id}`);
            alert("Película eliminada correctamente");
            cargarPeliculas();
        } catch (err) {
            console.error("Error al eliminar película:", err);
            alert("No se pudo eliminar la película");
        }
    };

    return (
        <div>
            <h2 className="mb-4">Gestión de Películas</h2>

            <div className="card shadow-sm p-4 mb-4">
                <h4 className="mb-3">
                    {modoEdicion ? "Editar Película" : "Agregar Película"}
                </h4>

                <form onSubmit={manejarSubmit}>
                    <div className="row">
                        <div className="col-md-6 mb-3">
                            <label className="form-label">Nombre original</label>
                            <input
                                type="text"
                                className="form-control"
                                name="nombreOriginal"
                                value={form.nombreOriginal}
                                onChange={manejarCambio}
                                required
                            />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Nombre comercial</label>
                            <input
                                type="text"
                                className="form-control"
                                name="nombreComercial"
                                value={form.nombreComercial}
                                onChange={manejarCambio}
                                required
                            />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Imagen</label>
                            <input
                                type="text"
                                className="form-control"
                                name="imagen"
                                value={form.imagen}
                                onChange={manejarCambio}
                                placeholder="Ejemplo: imagen.jpg o URL"
                            />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Duración (minutos)</label>
                            <input
                                type="number"
                                className="form-control"
                                name="duracion"
                                value={form.duracion}
                                onChange={manejarCambio}
                                required
                                min="1"
                            />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Protagonistas</label>
                            <input
                                type="text"
                                className="form-control"
                                name="protagonistas"
                                value={form.protagonistas}
                                onChange={manejarCambio}
                            />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Director</label>
                            <input
                                type="text"
                                className="form-control"
                                name="director"
                                value={form.director}
                                onChange={manejarCambio}
                            />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Clasificación</label>
                            <input
                                type="text"
                                className="form-control"
                                name="clasificacion"
                                value={form.clasificacion}
                                onChange={manejarCambio}
                                placeholder="Ejemplo: PG-13"
                            />
                        </div>
                    </div>

                    <div className="d-flex gap-2">
                        <button type="submit" className="btn btn-dark">
                            {modoEdicion ? "Actualizar" : "Guardar"}
                        </button>

                        <button
                            type="button"
                            className="btn btn-secondary"
                            onClick={limpiarFormulario}
                        >
                            Limpiar
                        </button>
                    </div>
                </form>
            </div>

            <div className="card shadow-sm p-4">
                <h4 className="mb-3">Lista de Películas</h4>

                {cargando && <p>Cargando películas...</p>}
                {error && <p className="text-danger">{error}</p>}

                {!cargando && !error && (
                    <div className="table-responsive">
                        <table className="table table-bordered table-hover align-middle">
                            <thead className="table-dark">
                                <tr>
                                    <th>ID</th>
                                    <th>Nombre comercial</th>
                                    <th>Nombre original</th>
                                    <th>Duración</th>
                                    <th>Director</th>
                                    <th>Clasificación</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>

                            <tbody>
                                {peliculas.length > 0 ? (
                                    peliculas.map((p) => (
                                        <tr key={p.id}>
                                            <td>{p.id}</td>
                                            <td>{p.nombreComercial}</td>
                                            <td>{p.nombreOriginal}</td>
                                            <td>{p.duracion} min</td>
                                            <td>{p.director}</td>
                                            <td>{p.clasificacion}</td>
                                            <td>
                                                <div className="d-flex gap-2">
                                                    <button
                                                        className="btn btn-warning btn-sm"
                                                        onClick={() => editarPelicula(p)}
                                                    >
                                                        Editar
                                                    </button>

                                                    <button
                                                        className="btn btn-danger btn-sm"
                                                        onClick={() => eliminarPelicula(p.id)}
                                                    >
                                                        Eliminar
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan="7" className="text-center">
                                            No hay películas registradas.
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
}

export default Peliculas;