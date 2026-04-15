# CineTEC

## Ejecutar frontend + backend al mismo tiempo

Si quieres levantar ambos servicios con un solo comando, usa:

```bash
./scripts/dev-all.sh
```

Este script:
- inicia `dotnet run` dentro de `CineTEC.API`
- inicia `npm run dev -- --host 0.0.0.0 --port 5173` dentro de `cinetec-admin`
- al presionar `Ctrl+C`, detiene ambos procesos

Requisitos:
- SDK de .NET instalado (para backend)
- Node.js + npm instalados (para frontend)