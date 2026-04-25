/*
DESCRIPCIÓN:
Este componente permite gestionar las películas del sistema (CRUD).
Incluye funcionalidades para listar, agregar, editar y eliminar películas,
interactuando con el backend mediante servicios.

ENTRADAS:
- Datos ingresados en el formulario (nombre, duración, director, etc.).
- Acciones del usuario (click en guardar, editar, eliminar).
- Respuestas del backend a través de los servicios.

SALIDAS:
- Renderización de la lista de películas en una tabla.
- Mensajes de éxito o error (alertas).
- Actualización visual de los datos al crear, editar o eliminar.

RESTRICCIONES:
- La duración debe ser numérica y mayor a 0.
- Los campos obligatorios deben completarse (required).
- Depende de los servicios (peliculasService) para funcionar.
- Requiere conexión con el backend.

Modificado por: Mario 
*/

import { useEffect, useState } from "react";
import {
    obtenerPeliculas,
    crearPelicula,
    actualizarPelicula,
    eliminarPeliculaService,
    subirImagenPelicula
} from "../services/peliculasService";

function Peliculas() {

    // Estados principales
    const [peliculas, setPeliculas] = useState([]); // Lista de películas
    const [cargando, setCargando] = useState(true); // Estado de carga
    const [error, setError] = useState(""); // Mensajes de error
    const [subiendoImagen, setSubiendoImagen] = useState(false); // Estado de subida de imagen

    // Estados para edición
    const [modoEdicion, setModoEdicion] = useState(false);
    const [idEditar, setIdEditar] = useState(null);

    // Estado del formulario
    const [form, setForm] = useState({
        nombreOriginal: "",
        nombreComercial: "",
        imagen: "",
        duracion: "",
        protagonistas: "",
        director: "",
        clasificacion: ""
    });

    // Se ejecuta al cargar el componente
    useEffect(() => {
        cargarPeliculas();
    }, []);

    // Función para obtener películas del backend
    const cargarPeliculas = async () => {
        try {
            setCargando(true);
            const data = await obtenerPeliculas();
            setPeliculas(data);
            setError("");
        } catch (err) {
            console.error(err);
            setError("No se pudieron cargar las películas.");
        } finally {
            setCargando(false);
        }
    };

    // Maneja cambios en inputs del formulario
    const manejarCambio = (e) => {
        const { name, value } = e.target;
        setForm({
            ...form,
            [name]: value
        });
    };

    const manejarSeleccionImagen = async (e) => {
        const archivo = e.target.files?.[0];
        if (!archivo) return;

        try {
            setSubiendoImagen(true);
            const respuesta = await subirImagenPelicula(archivo);
            setForm((prev) => ({
                ...prev,
                imagen: respuesta.ruta
            }));
        } catch (err) {
            console.error(err);
            alert("No se pudo subir la imagen");
        } finally {
            setSubiendoImagen(false);
            e.target.value = "";
        }
    };

    // Limpia el formulario y reinicia edición
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

    // Maneja envío del formulario (crear o actualizar)
    const manejarSubmit = async (e) => {
        e.preventDefault();
        setError("");

        const payload = {
            ...form,
            duracion: Number(form.duracion)
        };

        try {
            if (modoEdicion) {
                await actualizarPelicula(idEditar, payload);
                alert("Película actualizada correctamente");
            } else {
                await crearPelicula(payload);
                alert("Película agregada correctamente");
            }

            limpiarFormulario();
            cargarPeliculas();
        } catch (err) {
            console.error("Error completo:", err);

            const mensaje =
                err?.response?.data?.message ||
                err?.response?.data?.titulo ||
                err?.response?.data?.error ||
                err?.response?.data ||
                err?.message ||
                "Ocurrió un error al guardar la película";

            setError(`No se pudo guardar la película: ${mensaje}`);
            alert(`No se pudo guardar la película: ${mensaje}`);
        }
    };

    // Carga datos en el formulario para editar
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

    // Elimina una película
    const eliminarPelicula = async (id) => {
        if (!window.confirm("¿Desea eliminar esta película?")) return;

        try {
            await eliminarPeliculaService(id);
            alert("Película eliminada correctamente");
            cargarPeliculas();
        } catch (err) {
            console.error(err);
            alert("No se pudo eliminar la película");
        }
    };

    return (
        <div>

            {/* Título */}
            <h2 className="mb-4">Gestión de Películas</h2>

            {/* Formulario */}
            <div className="card shadow-sm p-4 mb-4">
                <h4 className="mb-3">
                    {modoEdicion ? "Editar Película" : "Agregar Película"}
                </h4>

                <form onSubmit={manejarSubmit}>
                    <div className="row">

                        {/* Campos del formulario */}
                        <div className="col-md-6 mb-3">
                            <label className="form-label">Nombre original</label>
                            <input type="text" className="form-control" name="nombreOriginal" value={form.nombreOriginal} onChange={manejarCambio} required />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Nombre comercial</label>
                            <input type="text" className="form-control" name="nombreComercial" value={form.nombreComercial} onChange={manejarCambio} required />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Imagen (ruta guardada)</label>
                            <input type="text" className="form-control" name="imagen" value={form.imagen} onChange={manejarCambio} placeholder="/images/peliculas/archivo.jpg" />
                            <small className="text-muted">
                                Puedes escribir una ruta manual o subir un archivo.
                            </small>
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Subir imagen</label>
                            <input type="file" className="form-control" accept=".jpg,.jpeg,.png,.webp" onChange={manejarSeleccionImagen} disabled={subiendoImagen} />
                            <small className="text-muted">
                                {subiendoImagen ? "Subiendo imagen..." : "Formatos permitidos: JPG, PNG, WEBP."}
                            </small>
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Duración</label>
                            <input type="number" className="form-control" name="duracion" value={form.duracion} onChange={manejarCambio} required min="1" />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Protagonistas</label>
                            <input type="text" className="form-control" name="protagonistas" value={form.protagonistas} onChange={manejarCambio} />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Director</label>
                            <input type="text" className="form-control" name="director" value={form.director} onChange={manejarCambio} />
                        </div>

                        <div className="col-md-6 mb-3">
                            <label className="form-label">Clasificación</label>
                            <input type="text" className="form-control" name="clasificacion" value={form.clasificacion} onChange={manejarCambio} />
                        </div>
                    </div>

                    {/* Botones */}
                    <div className="d-flex gap-2">
                        <button type="submit" className="btn btn-dark" disabled={subiendoImagen}>
                            {modoEdicion ? "Actualizar" : "Guardar"}
                        </button>

                        <button type="button" className="btn btn-secondary" onClick={limpiarFormulario}>
                            Limpiar
                        </button>
                    </div>
                </form>
            </div>

            {/* Tabla de películas */}
            <div className="card shadow-sm p-4">
                <h4 className="mb-3">Lista de Películas</h4>

                {/* Estados */}
                {cargando && <p>Cargando películas...</p>}
                {error && <p className="text-danger">{error}</p>}

                {/* Tabla */}
                {!cargando && !error && (
                    <div className="table-responsive">
                        <table className="table table-bordered table-hover align-middle">
                            <thead className="table-dark">
                                <tr>
                                    <th>ID</th>
                                    <th>Nombre comercial</th>
                                    <th>Nombre original</th>
                                    <th>Imagen</th>
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
                                            <td>{p.imagen ? <code>{p.imagen}</code> : "-"}</td>
                                            <td>{p.duracion} min</td>
                                            <td>{p.director}</td>
                                            <td>{p.clasificacion}</td>
                                            <td>
                                                <div className="d-flex gap-2">
                                                    <button className="btn btn-warning btn-sm" onClick={() => editarPelicula(p)}>
                                                        Editar
                                                    </button>
                                                    <button className="btn btn-danger btn-sm" onClick={() => eliminarPelicula(p.id)}>
                                                        Eliminar
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan="8" className="text-center">
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