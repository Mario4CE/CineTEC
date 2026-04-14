import { useEffect, useState } from "react";
import {
    obtenerSucursales,
    crearSucursal,
    actualizarSucursal,
    eliminarSucursalService
} from "../services/sucursalesService";

function Sucursales() {
    const [sucursales, setSucursales] = useState([]);
    const [modoEdicion, setModoEdicion] = useState(false);
    const [idEditar, setIdEditar] = useState(null);

    const [form, setForm] = useState({
        nombre: "",
        ubicacion: "",
        cantidadSalas: ""
    });

    useEffect(() => {
        cargarSucursales();
    }, []);

    const cargarSucursales = async () => {
        try {
            const data = await obtenerSucursales();
            setSucursales(data);
        } catch (error) {
            console.error(error);
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
            nombre: "",
            ubicacion: "",
            cantidadSalas: ""
        });
        setModoEdicion(false);
        setIdEditar(null);
    };

    const manejarSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            ...form,
            cantidadSalas: Number(form.cantidadSalas)
        };

        try {
            if (modoEdicion) {
                await actualizarSucursal(idEditar, payload);
                alert("Sucursal actualizada correctamente");
            } else {
                await crearSucursal(payload);
                alert("Sucursal agregada correctamente");
            }

            limpiarFormulario();
            cargarSucursales();
        } catch (error) {
            console.error(error);
            alert("Ocurrió un error al guardar la sucursal");
        }
    };

    const editarSucursal = (s) => {
        setModoEdicion(true);
        setIdEditar(s.id);
        setForm({
            nombre: s.nombre,
            ubicacion: s.ubicacion,
            cantidadSalas: s.cantidadSalas
        });
    };

    const eliminarSucursal = async (id) => {
        if (!window.confirm("¿Desea eliminar esta sucursal?")) return;

        try {
            await eliminarSucursalService(id);
            alert("Sucursal eliminada correctamente");
            cargarSucursales();
        } catch (error) {
            console.error(error);
            alert("No se pudo eliminar la sucursal");
        }
    };

    return (
        <div>
            <h2 className="mb-4">Gestión de Sucursales</h2>

            <div className="card shadow-sm p-4 mb-4">
                <h4 className="mb-3">
                    {modoEdicion ? "Editar Sucursal" : "Agregar Sucursal"}
                </h4>

                <form onSubmit={manejarSubmit}>
                    <div className="row">
                        <div className="col-md-4 mb-3">
                            <label className="form-label">Nombre</label>
                            <input type="text" className="form-control" name="nombre" value={form.nombre} onChange={manejarCambio} required />
                        </div>

                        <div className="col-md-4 mb-3">
                            <label className="form-label">Ubicación</label>
                            <input type="text" className="form-control" name="ubicacion" value={form.ubicacion} onChange={manejarCambio} required />
                        </div>

                        <div className="col-md-4 mb-3">
                            <label className="form-label">Cantidad de salas</label>
                            <input type="number" className="form-control" name="cantidadSalas" value={form.cantidadSalas} onChange={manejarCambio} required min="0" />
                        </div>
                    </div>

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

            <div className="card shadow-sm p-4">
                <h4 className="mb-3">Lista de Sucursales</h4>

                <div className="table-responsive">
                    <table className="table table-bordered table-hover align-middle">
                        <thead className="table-dark">
                            <tr>
                                <th>ID</th>
                                <th>Nombre</th>
                                <th>Ubicación</th>
                                <th>Cantidad de salas</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            {sucursales.length > 0 ? (
                                sucursales.map((s) => (
                                    <tr key={s.id}>
                                        <td>{s.id}</td>
                                        <td>{s.nombre}</td>
                                        <td>{s.ubicacion}</td>
                                        <td>{s.cantidadSalas}</td>
                                        <td>
                                            <div className="d-flex gap-2">
                                                <button className="btn btn-warning btn-sm" onClick={() => editarSucursal(s)}>
                                                    Editar
                                                </button>
                                                <button className="btn btn-danger btn-sm" onClick={() => eliminarSucursal(s.id)}>
                                                    Eliminar
                                                </button>
                                            </div>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="5" className="text-center">No hay sucursales registradas.</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}

export default Sucursales;