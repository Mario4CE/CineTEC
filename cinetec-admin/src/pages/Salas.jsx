/*
DESCRIPCIÓN:
Este componente permite gestionar las salas del sistema de cine.
Incluye funcionalidades para listar, crear, editar y eliminar salas,
asociándolas a una sucursal y calculando su capacidad automáticamente.

ENTRADAS:
- Identificador de la sala.
- Sucursal seleccionada (sucursalId).
- Número de filas.
- Número de columnas.
- Acciones del usuario (guardar, editar, eliminar).

SALIDAS:
- Renderización de la lista de salas en una tabla.
- Cálculo automático de la capacidad (filas × columnas).
- Mensajes de éxito o error (alert).
- Actualización de datos al crear, editar o eliminar.

RESTRICCIONES:
- Filas y columnas deben ser números mayores a 0.
- Se debe seleccionar una sucursal válida.
- Depende de servicios externos (salasService, sucursalesService).
- La capacidad es calculada automáticamente y no editable.

Modificado por: Mario 
*/

import { useEffect, useState } from "react";
import {
    obtenerSalas,
    crearSala,
    actualizarSala,
    eliminarSalaService
} from "../services/salasService";
import { obtenerSucursales } from "../services/sucursalesService";

function Salas() {

    // Estados principales
    const [salas, setSalas] = useState([]);
    const [sucursales, setSucursales] = useState([]);

    // Estados para edición
    const [modoEdicion, setModoEdicion] = useState(false);
    const [idEditar, setIdEditar] = useState(null);

    // Estado del formulario
    const [form, setForm] = useState({
        identificador: "",
        sucursalId: "",
        filas: "",
        columnas: ""
    });

    // Se ejecuta al cargar el componente
    useEffect(() => {
        cargarSalas();
        cargarSucursales();
    }, []);

    // Carga las salas
    const cargarSalas = async () => {
        try {
            const data = await obtenerSalas();
            setSalas(data);
        } catch (error) {
            console.error(error);
        }
    };

    // Carga las sucursales
    const cargarSucursales = async () => {
        try {
            const data = await obtenerSucursales();
            setSucursales(data);
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

    // Limpia el formulario y resetea edición
    const limpiarFormulario = () => {
        setForm({
            identificador: "",
            sucursalId: "",
            filas: "",
            columnas: ""
        });
        setModoEdicion(false);
        setIdEditar(null);
    };

    // Maneja el envío del formulario
    const manejarSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            identificador: form.identificador,
            sucursalId: Number(form.sucursalId),
            filas: Number(form.filas),
            columnas: Number(form.columnas)
        };

        try {
            if (modoEdicion) {
                // Actualizar sala
                await actualizarSala(idEditar, payload);
                alert("Sala actualizada correctamente");
            } else {
                // Crear nueva sala
                await crearSala(payload);
                alert("Sala agregada correctamente");
            }

            limpiarFormulario();
            cargarSalas();
            }catch (error) {
                console.error(error);

                const mensaje =
                    error?.response?.data?.mensaje || "Ocurrió un error al guardar la sala";

                alert(mensaje);
}
    };

    // Cargar datos en el formulario para editar
    const editarSala = (s) => {
        setModoEdicion(true);
        setIdEditar(s.id);

        setForm({
            identificador: s.identificador,
            sucursalId: s.sucursalId,
            filas: s.filas,
            columnas: s.columnas
        });
    };

    // Eliminar sala
    const eliminarSala = async (id) => {
        if (!window.confirm("¿Desea eliminar esta sala?")) return;

        try {
            await eliminarSalaService(id);
            alert("Sala eliminada correctamente");
            cargarSalas();
        } catch (error) {
            console.error(error);
            alert("No se pudo eliminar la sala");
        }
    };

    // Cálculo automático de capacidad
    const capacidad = (Number(form.filas) || 0) * (Number(form.columnas) || 0);

    return (
        <div>

            {/* Título */}
            <h2 className="mb-4">Gestión de Salas</h2>

            {/* Formulario */}
            <div className="card shadow-sm p-4 mb-4">
                <h4 className="mb-3">
                    {modoEdicion ? "Editar Sala" : "Agregar Sala"}
                </h4>

                <form onSubmit={manejarSubmit}>
                    <div className="row">

                        {/* Identificador */}
                        <div className="col-md-3 mb-3">
                            <label className="form-label">Identificador</label>
                            <input type="text" className="form-control" name="identificador" value={form.identificador} onChange={manejarCambio} required />
                        </div>

                        {/* Sucursal */}
                        <div className="col-md-3 mb-3">
                            <label className="form-label">Sucursal</label>
                            <select className="form-select" name="sucursalId" value={form.sucursalId} onChange={manejarCambio} required>
                                <option value="">Seleccione una sucursal</option>
                                {sucursales.map((s) => (
                                    <option key={s.id} value={s.id}>{s.nombre}</option>
                                ))}
                            </select>
                        </div>

                        {/* Filas */}
                        <div className="col-md-2 mb-3">
                            <label className="form-label">Filas</label>
                            <input type="number" className="form-control" name="filas" value={form.filas} onChange={manejarCambio} min="1" required />
                        </div>

                        {/* Columnas */}
                        <div className="col-md-2 mb-3">
                            <label className="form-label">Columnas</label>
                            <input type="number" className="form-control" name="columnas" value={form.columnas} onChange={manejarCambio} min="1" required />
                        </div>

                        {/* Capacidad (automática) */}
                        <div className="col-md-2 mb-3">
                            <label className="form-label">Capacidad</label>
                            <input type="number" className="form-control" value={capacidad} disabled />
                        </div>
                    </div>

                    {/* Botones */}
                    <div className="d-flex gap-2">
                        <button type="submit" className="btn btn-dark">
                            {modoEdicion ? "Actualizar" : "Guardar"}
                        </button>

                        <button type="button" className="btn btn-secondary" onClick={limpiarFormulario}>
                            Limpiar
                        </button>
                    </div>
                </form>
            </div>

            {/* Tabla */}
            <div className="card shadow-sm p-4">
                <h4 className="mb-3">Lista de Salas</h4>

                <div className="table-responsive">
                    <table className="table table-bordered table-hover align-middle">
                        <thead className="table-dark">
                            <tr>
                                <th>ID</th>
                                <th>Identificador</th>
                                <th>Sucursal</th>
                                <th>Filas</th>
                                <th>Columnas</th>
                                <th>Capacidad</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>

                        <tbody>
                            {salas.length > 0 ? (
                                salas.map((s) => (
                                    <tr key={s.id}>
                                        <td>{s.id}</td>
                                        <td>{s.identificador}</td>
                                        <td>{s.sucursal?.nombre}</td>
                                        <td>{s.filas}</td>
                                        <td>{s.columnas}</td>
                                        <td>{s.capacidad}</td>
                                        <td>
                                            <div className="d-flex gap-2">
                                                <button className="btn btn-warning btn-sm" onClick={() => editarSala(s)}>
                                                    Editar
                                                </button>
                                                <button className="btn btn-danger btn-sm" onClick={() => eliminarSala(s.id)}>
                                                    Eliminar
                                                </button>
                                            </div>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="7" className="text-center">
                                        No hay salas registradas.
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

export default Salas;