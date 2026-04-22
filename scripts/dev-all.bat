@echo off
setlocal enableextensions

title CineTEC - Launcher

echo ==============================================
echo   CineTEC - Iniciando entorno de desarrollo
echo ==============================================
echo.

REM Configuración centralizada de puertos/hosts
set API_PORT=5000
set ADMIN_PORT=5173
set CLIENT_PORT=8080
set BIND_HOST=0.0.0.0

REM Rutas
set ROOT=%~dp0..
set API_DIR=%ROOT%\CineTEC.API
set ADMIN_DIR=%ROOT%\cinetec-admin
set CLIENT_DIR=%ROOT%\cinetec-cliente

REM Validaciones básicas de herramientas
where dotnet >nul 2>nul
if errorlevel 1 (
  echo [ERROR] No se encontro dotnet en PATH.
  echo Instala .NET SDK y vuelve a ejecutar este script.
  echo.
  pause
  exit /b 1
)

where npm >nul 2>nul
if errorlevel 1 (
  echo [ERROR] No se encontro npm en PATH.
  echo Instala Node.js + npm y vuelve a ejecutar este script.
  echo.
  pause
  exit /b 1
)

if not exist "%API_DIR%" (
  echo [ERROR] No se encontro la carpeta del API: %API_DIR%
  echo Ejecuta este .bat desde la carpeta scripts del proyecto original.
  echo.
  pause
  exit /b 1
)

if not exist "%ADMIN_DIR%" (
  echo [ERROR] No se encontro la carpeta del admin: %ADMIN_DIR%
  echo.
  pause
  exit /b 1
)

if not exist "%CLIENT_DIR%" (
  echo [ERROR] No se encontro la carpeta del cliente: %CLIENT_DIR%
  echo.
  pause
  exit /b 1
)

echo [OK] Requisitos basicos detectados.
echo Abriendo 3 ventanas: API, Admin, Cliente...
echo.

start "CineTEC API" cmd /k "cd /d %API_DIR% && set ASPNETCORE_ENVIRONMENT=Development && dotnet run --no-launch-profile --urls=http://%BIND_HOST%:%API_PORT%"
start "CineTEC Admin" cmd /k "cd /d %ADMIN_DIR% && npm run dev -- --host %BIND_HOST% --port %ADMIN_PORT%"

REM Usa npx para no depender de live-server global
start "CineTEC Cliente" cmd /k "cd /d %CLIENT_DIR% && npx --yes live-server --port=%CLIENT_PORT% --host=%BIND_HOST%"

echo ==============================================
echo Servicios lanzados
echo ==============================================
echo API:     http://localhost:%API_PORT%
echo Admin:   http://localhost:%ADMIN_PORT%
echo Cliente: http://localhost:%CLIENT_PORT%
echo.
echo Para otro dispositivo en la misma red, usa la IP de tu PC:
echo - Cliente: http://IP_DE_TU_PC:%CLIENT_PORT%
echo - API:     http://IP_DE_TU_PC:%API_PORT%
echo.
echo IMPORTANTE: no abras http://0.0.0.0:%CLIENT_PORT% en el navegador.
echo.
echo Abriendo login del cliente en tu navegador...
timeout /t 4 /nobreak >nul
start "" "http://localhost:%CLIENT_PORT%/index.html"
echo.
pause