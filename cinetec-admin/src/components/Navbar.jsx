/*
DESCRIPCIÓN:
Este componente representa la barra superior (Navbar) del panel administrativo.
Muestra el título del sistema y forma parte de la estructura visual del Layout.

ENTRADAS:
- No recibe entradas directas (no props).

SALIDAS:
- Renderiza un encabezado con el título del panel administrativo.

RESTRICCIONES:
- Debe ser utilizado dentro del Layout del sistema.
- Es un componente visual, no maneja lógica ni estado.

Modificado por: Mario
*/

function Navbar() {
    return (
        // Contenedor de la barra superior
        <div className="bg-white shadow-sm px-4 py-3 border-bottom">

            {/* Título del panel */}
            <h4 className="m-0">Panel de Administración</h4>

        </div>
    )
}

export default Navbar