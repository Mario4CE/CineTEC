# React + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Oxc](https://oxc.rs)
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/)

## React Compiler

The React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).

## Expanding the ESLint configuration

If you are developing a production application, we recommend using TypeScript with type-aware lint rules enabled. Check out the [TS template](https://github.com/vitejs/vite/tree/main/packages/create-vite/template-react-ts) for information on how to integrate TypeScript and [`typescript-eslint`](https://typescript-eslint.io) in your project.

## Acceso desde otros dispositivos en la misma red

Para que el frontend no quede limitado a `localhost`:

1. Ejecuta en desarrollo con:
   - `npm run dev -- --host 0.0.0.0 --port 5173`
2. O para modo preview (build):
   - `npm run build`
   - `npm run preview -- --host 0.0.0.0 --port 4173`
3. En otro equipo de la misma red, abre:
   - `http://IP_DE_TU_PC:5173` (dev)
   - `http://IP_DE_TU_PC:4173` (preview)
4. Configura la API con `VITE_API_URL` en un archivo `.env`.

> Nota: asegúrate de permitir los puertos en el firewall y de exponer también el backend en `0.0.0.0:5000`.