import { useEffect, useState } from 'react'

function Clientes() {
    const [clientes, setClientes] = useState([])
    const [busqueda, setBusqueda] = useState('')
    const [clienteEditando, setClienteEditando] = useState(null)
    const [error, setError] = useState('')

    useEffect(() => {
        cargarClientes()
    }, [])

    const cargarClientes = () => {
        /*
            Aquí luego se cargan los clientes desde la API.

            Ejemplo:
            const response = await axios.get('/admin/clientes')
            setClientes(response.data)
        */

        setClientes([])
    }

    const calcularEdad = (fechaNacimiento) => {
        if (!fechaNacimiento) return ''

        const hoy = new Date()
        const nacimiento = new Date(fechaNacimiento)

        let edad = hoy.getFullYear() - nacimiento.getFullYear()
        const mes = hoy.getMonth() - nacimiento.getMonth()

        if (mes < 0 || (mes === 0 && hoy.getDate() < nacimiento.getDate())) {
            edad--
        }

        return edad
    }

    const manejarCambioEdicion = (e) => {
        const { name, value } = e.target

        if (name === 'fechaNacimiento') {
            setClienteEditando({
                ...clienteEditando,
                fechaNacimiento: value,
                edad: calcularEdad(value)
            })
        } else {
            setClienteEditando({
                ...clienteEditando,
                [name]: value
            })
        }
    }

    const guardarCambios = (e) => {
        e.preventDefault()
        setError('')

        if (
            !clienteEditando.nombre ||
            !clienteEditando.cedula ||
            !clienteEditando.telefono ||
            !clienteEditando.fechaNacimiento
        ) {
            setError('Todos los campos son obligatorios.')
            return
        }

        /*
            Aquí luego se actualiza el cliente en la API.

            Ejemplo:
            await axios.put(`/admin/clientes/${clienteEditando.id}`, clienteEditando)
        */

        const clientesActualizados = clientes.map((cliente) =>
            cliente.id === clienteEditando.id ? clienteEditando : cliente
        )

        setClientes(clientesActualizados)
        setClienteEditando(null)
    }

    const eliminarCliente = (id) => {
        /*
            Aquí luego se elimina el cliente en la API.

            Ejemplo:
            await axios.delete(`/admin/clientes/${id}`)
        */

        const clientesFiltrados = clientes.filter((cliente) => cliente.id !== id)
        setClientes(clientesFiltrados)
    }

    const clientesFiltrados = clientes.filter((cliente) =>
        cliente.nombre?.toLowerCase().includes(busqueda.toLowerCase()) ||
        cliente.cedula?.includes(busqueda) ||
        cliente.telefono?.includes(busqueda)
    )

    return (
        <div className="p-6 bg-gray-100 min-h-screen">

            <div className="bg-white rounded-xl shadow p-6 max-w-6xl mx-auto">

                <h1 className="text-2xl font-bold text-gray-800 mb-2">
                    Administración de Clientes
                </h1>

                <p className="text-gray-600 mb-6">
                    Permite al administrador gestionar la información de los clientes
                    registrados en el sistema, incluyendo nombre, cédula, teléfono,
                    fecha de nacimiento y edad.
                </p>

                <div className="mb-6">
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                        Buscar cliente
                    </label>

                    <input
                        type="text"
                        value={busqueda}
                        onChange={(e) => setBusqueda(e.target.value)}
                        placeholder="Buscar por nombre, cédula o teléfono"
                        className="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                </div>

                <div className="overflow-x-auto">
                    <table className="w-full border-collapse bg-white">
                        <thead>
                            <tr className="bg-gray-200 text-gray-700">
                                <th className="border px-4 py-2 text-left">Nombre</th>
                                <th className="border px-4 py-2 text-left">Cédula</th>
                                <th className="border px-4 py-2 text-left">Teléfono</th>
                                <th className="border px-4 py-2 text-left">Fecha nacimiento</th>
                                <th className="border px-4 py-2 text-left">Edad</th>
                                <th className="border px-4 py-2 text-center">Acciones</th>
                            </tr>
                        </thead>

                        <tbody>
                            {clientesFiltrados.length === 0 ? (
                                <tr>
                                    <td
                                        colSpan="6"
                                        className="border px-4 py-4 text-center text-gray-500"
                                    >
                                        No hay clientes registrados.
                                    </td>
                                </tr>
                            ) : (
                                clientesFiltrados.map((cliente) => (
                                    <tr key={cliente.id}>
                                        <td className="border px-4 py-2">
                                            {cliente.nombre}
                                        </td>

                                        <td className="border px-4 py-2">
                                            {cliente.cedula}
                                        </td>

                                        <td className="border px-4 py-2">
                                            {cliente.telefono}
                                        </td>

                                        <td className="border px-4 py-2">
                                            {cliente.fechaNacimiento}
                                        </td>

                                        <td className="border px-4 py-2">
                                            {cliente.edad}
                                        </td>

                                        <td className="border px-4 py-2 text-center">
                                            <button
                                                onClick={() => setClienteEditando(cliente)}
                                                className="bg-yellow-500 text-white px-3 py-1 rounded-lg mr-2 hover:bg-yellow-600 transition"
                                            >
                                                Editar
                                            </button>

                                            <button
                                                onClick={() => eliminarCliente(cliente.id)}
                                                className="bg-red-600 text-white px-3 py-1 rounded-lg hover:bg-red-700 transition"
                                            >
                                                Eliminar
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>

                {clienteEditando && (
                    <div className="mt-8 bg-gray-50 border rounded-xl p-5">

                        <h2 className="text-xl font-semibold text-gray-800 mb-4">
                            Editar información del cliente
                        </h2>

                        <form onSubmit={guardarCambios} className="grid grid-cols-1 md:grid-cols-2 gap-4">

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                    Nombre
                                </label>

                                <input
                                    type="text"
                                    name="nombre"
                                    value={clienteEditando.nombre}
                                    onChange={manejarCambioEdicion}
                                    className="w-full border rounded-lg px-4 py-2"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                    Cédula
                                </label>

                                <input
                                    type="text"
                                    name="cedula"
                                    value={clienteEditando.cedula}
                                    onChange={manejarCambioEdicion}
                                    className="w-full border rounded-lg px-4 py-2"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                    Teléfono
                                </label>

                                <input
                                    type="text"
                                    name="telefono"
                                    value={clienteEditando.telefono}
                                    onChange={manejarCambioEdicion}
                                    className="w-full border rounded-lg px-4 py-2"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                    Fecha de nacimiento
                                </label>

                                <input
                                    type="date"
                                    name="fechaNacimiento"
                                    value={clienteEditando.fechaNacimiento}
                                    onChange={manejarCambioEdicion}
                                    className="w-full border rounded-lg px-4 py-2"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-1">
                                    Edad
                                </label>

                                <input
                                    type="number"
                                    value={clienteEditando.edad}
                                    readOnly
                                    className="w-full border rounded-lg px-4 py-2 bg-gray-100"
                                />
                            </div>

                            {error && (
                                <div className="md:col-span-2 bg-red-100 text-red-700 px-4 py-2 rounded-lg text-sm">
                                    {error}
                                </div>
                            )}

                            <div className="md:col-span-2 flex gap-3 mt-2">
                                <button
                                    type="submit"
                                    className="bg-blue-600 text-white px-5 py-2 rounded-lg hover:bg-blue-700 transition"
                                >
                                    Guardar cambios
                                </button>

                                <button
                                    type="button"
                                    onClick={() => setClienteEditando(null)}
                                    className="bg-gray-500 text-white px-5 py-2 rounded-lg hover:bg-gray-600 transition"
                                >
                                    Cancelar
                                </button>
                            </div>
                        </form>
                    </div>
                )}
            </div>
        </div>
    )
}

export default Clientes