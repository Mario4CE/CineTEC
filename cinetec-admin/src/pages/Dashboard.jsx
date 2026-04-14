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
        // Contenedor con padding
        <div className="p-4">

            {/* Título del panel */}
            <h2>Dashboard</h2>

            {/* Mensaje de bienvenida */}
            <p>Bienvenido al sistema CineTEC</p>
        </div>
    )
}

export default Dashboard