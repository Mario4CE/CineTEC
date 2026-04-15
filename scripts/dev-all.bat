@echo off
setlocal

start "CineTEC API" cmd /k "cd /d %~dp0..\CineTEC.API && dotnet run"
start "CineTEC Admin" cmd /k "cd /d %~dp0..\cinetec-admin && npm run dev -- --host 0.0.0.0 --port 5173"

echo.
echo Backend y Frontend iniciados en ventanas separadas.
echo Si 5173 esta ocupado, Vite cambiara a 5174/5175 automaticamente.
pause