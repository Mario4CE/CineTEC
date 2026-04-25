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

start "CineTEC API" cmd /k "cd /d %API_DIR% && set ASPNETCORE_ENVIRONMENT=Development && dotnet run --no-launch-profile --urls=http://%BIND_HOST%:%API_PORT%"
start "CineTEC Admin" cmd /k "cd /d %ADMIN_DIR% && npm run dev -- --host %BIND_HOST% --port %ADMIN_PORT% --open"

REM Usa npx para no depender de live-server global
start "CineTEC Cliente" cmd /k "cd /d %CLIENT_DIR% && npx --yes live-server --port=%CLIENT_PORT% --host=%BIND_HOST%"

pause