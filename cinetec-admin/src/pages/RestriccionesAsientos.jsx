import { useState } from 'react'

function RestriccionesAsientos() {
    const [porcentajePermitido, setPorcentajePermitido] = useState('')
    const [capacidadTotal, setCapacidadTotal] = useState('')
    const [aforoMaximo, setAforoMaximo] = useState(null)
    const [error, setError] = useState('')

    const calcularAforo = (e) => {
        e.preventDefault()
        setError('')

        const porcentaje = Number(porcentajePermitido)
        const capacidad = Number(capacidadTotal)

        if (!porcentajePermitido || !capacidadTotal) {
            setError('Debe ingresar todos los campos.')
            return
        }

        if (porcentaje <= 0 || porcentaje > 100) {
            setError('El porcentaje permitido debe estar entre 1 y 100.')
            return
        }

        if (capacidad <= 0) {
            setError('La capacidad total debe ser mayor a 0.')
            return
        }

        const resultado = Math.floor((capacidad * porcentaje) / 100)
        setAforoMaximo(resultado)
    }

    const guardarRestriccion = () => {
        /*
            Aquí luego puedes conectar con la API.

            Ejemplo:
            await axios.post('/admin/restricciones-asientos', {
                porcentajePermitido,
                capacidadTotal,
                aforoMaximo
            })
        */

        alert('Restricción guardada correctamente')
    }

    return (
        <div className="p-6 bg-gray-100 min-h-screen">

            <div className="bg-white rounded-xl shadow p-6 max-w-3xl mx-auto">

                <h1 className="text-2xl font-bold text-gray-800 mb-2">
                    Restricciones de Asientos
                </h1>

                <p className="text-gray-600 mb-6">
                    Administración de restricciones sanitarias CoTec-23 para limitar
                    la capacidad máxima permitida según lo indicado por el Ministerio
                    de Salud.
                </p>

                <form onSubmit={calcularAforo} className="space-y-4">

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Porcentaje de capacidad permitido
                        </label>

                        <input
                            type="number"
                            value={porcentajePermitido}
                            onChange={(e) => setPorcentajePermitido(e.target.value)}
                            placeholder="Ej: 50"
                            className="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        />

                        <p className="text-sm text-gray-500 mt-1">
                            Si el Ministerio permite un 50% de aforo, ingrese 50.
                        </p>
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Capacidad total de la sala
                        </label>

                        <input
                            type="number"
                            value={capacidadTotal}
                            onChange={(e) => setCapacidadTotal(e.target.value)}
                            placeholder="Ej: 120"
                            className="w-full border rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        />
                    </div>

                    {error && (
                        <div className="bg-red-100 text-red-700 px-4 py-2 rounded-lg text-sm">
                            {error}
                        </div>
                    )}

                    <button
                        type="submit"
                        className="bg-blue-600 text-white px-5 py-2 rounded-lg hover:bg-blue-700 transition"
                    >
                        Calcular aforo permitido
                    </button>
                </form>

                {aforoMaximo !== null && (
                    <div className="mt-6 bg-gray-50 border rounded-lg p-5">

                        <h2 className="text-lg font-semibold text-gray-800 mb-2">
                            Resultado
                        </h2>

                        <p className="text-gray-700">
                            Capacidad total de la sala:
                            <span className="font-semibold"> {capacidadTotal} asientos</span>
                        </p>

                        <p className="text-gray-700">
                            Porcentaje permitido:
                            <span className="font-semibold"> {porcentajePermitido}%</span>
                        </p>

                        <p className="text-gray-700 mt-2">
                            Aforo máximo permitido:
                            <span className="font-bold text-blue-600"> {aforoMaximo} asientos</span>
                        </p>

                        <button
                            onClick={guardarRestriccion}
                            className="mt-4 bg-green-600 text-white px-5 py-2 rounded-lg hover:bg-green-700 transition"
                        >
                            Guardar restricción
                        </button>
                    </div>
                )}
            </div>
        </div>
    )
}

export default RestriccionesAsientos