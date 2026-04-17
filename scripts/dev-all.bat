@echo off
setlocal

start "CineTEC API" cmd /k "cd /d %~dp0..\CineTEC.API && dotnet run"
start "CineTEC Admin" cmd /k "cd /d %~dp0..\cinetec-admin && npm run dev -- --host 0.0.0.0 --port 5173"
start "CineTEC Cliente" cmd /k "cd /d %~dp0..\cinetec-cliente && live-server --port=8080 --host=0.0.0.0"

echo.
echo Backend, Frontend Admin y Cliente iniciados en ventanas separadas.
echo API:           http://localhost:5000
echo Admin:         http://localhost:5173
echo Cliente:       http://localhost:8080
echo.
echo Si necesitas instalar live-server, ejecuta:
echo npm install -g live-server
echo.
pause