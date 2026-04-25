/*
DESCRIPCIÓN:
Este componente representa el panel principal (Dashboard) del sistema.
Muestra un mensaje de bienvenida al usuario dentro del entorno administrativo.

ENTRADAS:
- No recibe entradas directas (no props, no formularios).

SALIDAS:
- Renderiza un título y un mensaje de bienvenida en pantalla.

RESTRICCIONES:
- Solo debe mostrarse después de iniciar sesión correctamente.
- Depende del sistema de rutas para ser accedido (ruta /admin).

Modificado por: Mario 
*/

function Dashboard() {
    return (
        <div className="p-6 bg-gray-100 min-h-screen">

            {/* Bienvenida */}
            <div className="bg-white rounded-xl shadow p-6 border border-gray-200 max-w-4xl">
                <h2 className="text-2xl font-bold text-gray-800 mb-2">
                    Bienvenido al sistema CineTEC 🎬
                </h2>

                <p className="text-gray-600 mb-5">
                    Desde este panel puedes administrar la información principal del sistema,
                    como películas, sucursales, salas, clientes, restricciones de asientos y proyecciones.
                </p>

                {/* Indicaciones */}
                <div className="mb-5">
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">
                        Indicaciones
                    </h3>

                    <ul className="list-disc list-inside text-gray-600 space-y-1 text-sm">
                        <li>Utiliza el menú lateral para navegar entre los módulos.</li>
                        <li>Verifica los datos antes de registrar nueva información.</li>
                        <li>Antes de crear una proyección, asegúrate de que la película y la sala existan.</li>
                        <li>Revisa que las fechas y horarios de las funciones sean correctos.</li>
                    </ul>
                </div>

                {/* Recomendaciones */}
                <div className="bg-blue-50 border border-blue-200 rounded-lg p-4">
                    <h3 className="text-lg font-semibold text-blue-800 mb-2">
                        Recomendaciones
                    </h3>

                    <p className="text-sm text-blue-700">
                        Mantén actualizada la información del sistema y evita eliminar registros
                        sin confirmar que no estén relacionados con otros datos importantes.
                    </p>
                </div>
            </div>

        </div>
    )
}

export default Dashboard