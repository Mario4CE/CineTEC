# CineTEC
 
-## Ejecutar frontend + backend al mismo tiempo
+Guía rápida para dejar funcionando **backend + admin + cliente** en tu máquina.
 
-Si quieres levantar ambos servicios con un solo comando, usa:
+## 1) Requisitos
+
+Instala lo siguiente antes de correr el proyecto:
+
+- **.NET SDK 10** (el API usa `net10.0`).
+- **SQL Server LocalDB** (o SQL Server Express/Developer).
+- **Node.js + npm** (para el panel admin en React/Vite).
+- **live-server** para levantar el cliente estático:
+  ```bash
+  npm install -g live-server
+  ```
+
+> Si no quieres instalación global, también puedes abrir `cinetec-cliente/*.html` directamente en navegador, pero para desarrollo se recomienda `live-server`.
+
+## 2) Configurar la base de datos
+
+1. Revisa la cadena de conexión en `CineTEC.API/appsettings.json`.
+2. Por defecto apunta a:
+   - `Server=(localdb)\\MSSQLLocalDB`
+   - `Database=CineTecDB`
+3. Si usas otra instancia o nombre de DB, actualiza `DefaultConnection`.
+
+### Script SQL opcional
+
+En `scripts/01_CreateUsersTable.sql` hay un script para crear la tabla `Usuarios` e índice en `Cedula`.
+
+> **Importante:** ese script usa `USE CineTecBD;` (termina en **BD**). Si tu DB es `CineTecDB` (como en `appsettings.json`), ajusta el nombre antes de ejecutarlo.
+
+## 3) Instalar dependencias del frontend admin
+
+Desde la raíz del repo:
 
 ```bash
-./scripts/dev-all.sh
+cd cinetec-admin
+npm install
+```
+
+## 4) Levantar el proyecto
+
+Tienes dos formas: automática (Windows) o manual (cualquier SO).
+
+### Opción A: script automático (Windows)
+
+```bat
+scripts\dev-all.bat
+```
+
+Este script abre 3 ventanas y levanta:
+
+- API: `http://localhost:5000`
+- Admin (Vite): `http://localhost:5173`
+- Cliente: `http://localhost:8080`
+
+### Opción B: manual (Windows/macOS/Linux)
+
+Abre 3 terminales:
+
+1. **API**
+   ```bash
+   cd CineTEC.API
+   dotnet run
+   ```
+
+2. **Admin**
+   ```bash
+   cd cinetec-admin
+   npm run dev -- --host 0.0.0.0 --port 5173
+   ```
+
+3. **Cliente**
+   ```bash
+   cd cinetec-cliente
+   live-server --port=8080 --host=0.0.0.0
+   ```
+
+## 5) URLs de trabajo
+
+- Swagger/API: `http://localhost:5000/swagger`
+- Admin: `http://localhost:5173`
+- Cliente: `http://localhost:8080`
+
+## 6) Configuración de API para el admin
+
+El admin consume la API desde:
+
+- `VITE_API_URL` (si existe en `.env`), o
+- `http://localhost:5000/api` por defecto. O pon tu direccion ip de tu dispositivo
+
+Ejemplo en `cinetec-admin/.env`:
+
+```env
+VITE_API_URL=http://localhost:5000/api
 ```
 
-Este script:
-- inicia `dotnet run` dentro de `CineTEC.API`
-- inicia `npm run dev -- --host 0.0.0.0 --port 5173` dentro de `cinetec-admin`
-- al presionar `Ctrl+C`, detiene ambos procesos
+## 7) Problemas comunes
 
-Requisitos:
-- SDK de .NET instalado (para backend)
-- Node.js + npm instalados (para frontend)



+- **No conecta a la DB:** verifica que LocalDB/SQL Server esté instalado y la cadena de conexión sea correcta.
+- **Puerto ocupado (5000, 5173, 8080):** cambia puertos en los comandos y/o configuración.
+- **`live-server` no reconocido:** instala globalmente con `npm install -g live-server`.
+- **CORS o acceso desde otro dispositivo:** asegúrate de exponer host `0.0.0.0` y abrir firewall.
 