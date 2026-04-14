/*
DESCRIPCIÓN:
Este componente permite gestionar las proyecciones del sistema de cine.
Incluye funcionalidades para listar, crear y eliminar proyecciones,
relacionando películas, salas y fechas.

ENTRADAS:
- Selección de película (peliculaId).
- Selección de sala (salaId).
- Fecha y hora de la proyección.
- Acciones del usuario (guardar, eliminar).

SALIDAS:
- Renderización de la lista de proyecciones en tabla.
- Mensajes de confirmación o error (alert).
- Actualización dinámica de datos al crear o eliminar.

RESTRICCIONES:
- Debe seleccionarse una película y una sala válidas.
- La fecha es obligatoria.
- Depende de servicios externos (peliculasService, salasService, proyeccionesService).
- Requiere que existan películas y salas registradas previamente.

Modificado por: Mario 
*/

import { useEffect, useState } from "react";
import { obtenerPeliculas } from "../services/peliculasService";
import { obtenerSalas } from "../services/salasService";
import {
    obtenerProyecciones,
    crearProyeccion,
    eliminarProyeccionService
} from "../services/proyeccionesService";

function Proyecciones() {

    // Estados principales
    const [proyecciones, setProyecciones] = useState([]);
    const [peliculas, setPeliculas] = useState([]);
    const [salas, setSalas] = useState([]);

    // Estado del formulario
    const [form, setForm] = useState({
        peliculaId: "",
        salaId: "",
        fecha: ""
    });

    // Se ejecuta al cargar el componente
    useEffect(() => {
        cargarProyecciones();
        cargarPeliculas();
        cargarSalas();
    }, []);

    // Carga las proyecciones
    const cargarProyecciones = async () => {
        try {
            const data = await obtenerProyecciones();
            setProyecciones(data);
        } catch (error) {
            console.error(error);
        }
    };

    // Carga las películas
    const cargarPeliculas = async () => {
        try {
            const data = await obtenerPeliculas();
            setPeliculas(data);
        } catch (error) {
            console.error(error);
        }
    };

    // Carga las salas
    const cargarSalas = async () => {
        try {
            const data = await obtenerSalas();
            setSalas(data);
        } catch (error) {
            console.error(error);
        }
    };

    // Maneja cambios en el formulario
    const manejarCambio = (e) => {
        const { name, value } = e.target;
        setForm({
            ...form,
            [name]: value
        });
    };

    // Limpia el formulario
    const limpiarFormulario = () => {
        setForm({
            peliculaId: "",
            salaId: "",
            fecha: ""
        });
    };

    // Maneja el envío del formulario
    const manejarSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            peliculaId: Number(form.peliculaId),
            salaId: Number(form.salaId),
            fecha: form.fecha
        };

        try {
            await crearProyeccion(payload);
            alert("Proyección agregada correctamente");
            limpiarFormulario();
            cargarProyecciones();
        } catch (error) {
            console.error(error);
            alert("Ocurrió un error al guardar la proyección");
        }
    };

    // Elimina una proyección
    const eliminarProyeccion = async (id) => {
        if (!window.confirm("¿Desea eliminar esta proyección?")) return;

        try {
            await eliminarProyeccionService(id);
            alert("Proyección eliminada correctamente");
            cargarProyecciones();
        } catch (error) {
            console.error(error);
            alert("No se pudo eliminar la proyección");
        }
    };

    return (
        <div>

            {/* Título */}
            <h2 className="mb-4">Gestión de Proyecciones</h2>

            {/* Formulario */}
            <div className="card shadow-sm p-4 mb-4">
                <h4 className="mb-3">Agregar Proyección</h4>

                <form onSubmit={manejarSubmit}>
                    <div className="row">

                        {/* Selección de película */}
                        <div className="col-md-4 mb-3">
                            <label className="form-label">Película</label>
                            <select className="form-select" name="peliculaId" value={form.peliculaId} onChange={manejarCambio} required>
                                <option value="">Seleccione una película</option>
                                {peliculas.map((p) => (
                                    <option key={p.id} value={p.id}>{p.nombreComercial}</option>
                                ))}
                            </select>
                        </div>

                        {/* Selección de sala */}
                        <div className="col-md-4 mb-3">
                            <label className="form-label">Sala</label>
                            <select className="form-select" name="salaId" value={form.salaId} onChange={manejarCambio} required>
                                <option value="">Seleccione una sala</option>
                                {salas.map((s) => (
                                    <option key={s.id} value={s.id}>
                                        {s.identificador} - {s.sucursal?.nombre}
                                    </option>
                                ))}
                            </select>
                        </div>

                        {/* Fecha y hora */}
                        <div className="col-md-4 mb-3">
                            <label className="form-label">Fecha y hora</label>
                            <input type="datetime-local" className="form-control" name="fecha" value={form.fecha} onChange={manejarCambio} required />
                        </div>
                    </div>

                    {/* Botones */}
                    <div className="d-flex gap-2">
                        <button type="submit" className="btn btn-dark">Guardar</button>
                        <button type="button" className="btn btn-secondary" onClick={limpiarFormulario}>
                            Limpiar
                        </button>
                    </div>
                </form>
            </div>

            {/* Tabla */}
            <div className="card shadow-sm p-4">
                <h4 className="mb-3">Lista de Proyecciones</h4>

                <div className="table-responsive">
                    <table className="table table-bordered table-hover align-middle">
                        <thead className="table-dark">
                            <tr>
                                <th>ID</th>
                                <th>Película</th>
                                <th>Sala</th>
                                <th>Sucursal</th>
                                <th>Fecha y hora</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>

                        <tbody>
                            {proyecciones.length > 0 ? (
                                proyecciones.map((p) => (
                                    <tr key={p.id}>
                                        <td>{p.id}</td>
                                        <td>{p.pelicula?.nombreComercial}</td>
                                        <td>{p.sala?.identificador}</td>
                                        <td>{p.sala?.sucursal?.nombre}</td>
                                        <td>{new Date(p.fecha).toLocaleString()}</td>
                                        <td>
                                            <button className="btn btn-danger btn-sm" onClick={() => eliminarProyeccion(p.id)}>
                                                Eliminar
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="6" className="text-center">
                                        No hay proyecciones registradas.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}

export default Proyecciones;