# 🔧 CineTEC Mobile - Guía de Conexión y Troubleshooting

## ✅ Cambios Realizados

### 1. **Servicio API Centralizado** (`ApiService.cs`)
- ✅ HttpClient compartido (más eficiente)
- ✅ Logging automático de requests/responses
- ✅ Manejo robusto de errores
- ✅ Deserialization automática con PropertyNameCaseInsensitive
- ✅ Timeout de 30 segundos

### 2. **Servicio de Usuario** (`UsuarioService.cs`)
- ✅ Método `ObtenerPorCedula(cedula)` 
- ✅ Método `Registrar(usuarioDto)`
- ✅ Logging de cada operación

### 3. **Inyección de Dependencias** (`MauiProgram.cs`)
- ✅ `AddHttpClient<ApiService>()` - HttpClient con HttpClientFactory
- ✅ `AddScoped<UsuarioService>()` - Servicio de usuarios
- ✅ `AddScoped<CineService>()` - Servicio de cines

### 4. **LoginPage Mejorado** 
- ✅ Usa inyección de dependencias
- ✅ Logging con ILogger
- ✅ Indicador de carga (botón deshabilitado)
- ✅ Mensajes de error descriptivos
- ✅ Manejo específico de errores HTTP

### 5. **CineService Actualizado**
- ✅ Usa ApiService centralizado
- ✅ Elimina código duplicado
- ✅ Mismo manejo de errores consistente

---

## 🌐 Configuración de IP

**IP Actual: `172.18.92.169:5000`**

Para cambiar la IP:
1. Abre `Services/ApiService.cs`
2. Busca la línea: `private const string BaseApiUrl = "http://172.18.92.169:5000/api";`
3. Cambia `172.18.92.169` por la IP correcta

### Para obtener tu IP:
```powershell
ipconfig | findstr /I "ipv4"
```

---

## 🚀 Pasos para Conectar

### En tu PC:
1. **Asegúrate que el API esté corriendo:**
   ```
   dotnet run --project CineTEC.API
   ```
   Debe mostrar: `Now listening on: http://localhost:5000`

2. **Verifica que SQL Server/LocalDB esté disponible:**
   - Si usas LocalDB: Debe estar en `(localdb)\MSSQLLocalDB`
   - Si usas otro server: Actualiza `appsettings.json`

3. **Prueba el endpoint directamente** en Postman o navegador:
   ```
   GET http://172.18.92.169:5000/api/usuarios/cedula/123456789
   ```

### En el Emulador Android:
1. **Abre la app**
2. **Ingresa una cédula válida** (que exista en la BD)
3. **Si falla**, verás mensajes de error detallados

---

## 🐛 Troubleshooting

### Error: "No se pudo conectar"
**Causas posibles:**
- [ ] API no está corriendo
- [ ] IP incorrecta (usa `ipconfig`)
- [ ] Puerto incorrecto (debe ser 5000)
- [ ] Firewall bloqueando conexión
- [ ] Emulador sin acceso a la red

**Soluciones:**
1. Verifica que el API esté corriendo: `dotnet run`
2. Verifica la IP: `ipconfig` y actualiza en `ApiService.cs`
3. Abre puerto 5000 en Firewall de Windows
4. En emulador, intenta con IP real de PC (NO localhost)

### Error: "Usuario no encontrado"
- [ ] La cédula no existe en la base de datos
- [ ] Verifica que haya datos en tabla `Usuarios`
- [ ] Prueba en Swagger: `http://localhost:5000/swagger`

### Error: "JSON Deserialization Error"
- [ ] El modelo `Usuario` no coincide con la respuesta
- [ ] Revisa `Models/Usuario.cs`
- [ ] Verifica que tenga propiedades `NombreCompleto` y `Cedula`

### Conexión muy lenta
- [ ] Aumenta timeout (actualmente 30s)
- [ ] Edita en `ApiService.cs`: `_httpClient.Timeout = TimeSpan.FromSeconds(60);`

---

## 📊 Verificar Datos en BD

Desde Visual Studio, abre:
**View → SQL Server Object Explorer**

Navega a:
```
SQL Server
  → (localdb)\MSSQLLocalDB  
    → Databases
      → CineTecDB
        → Tables
          → dbo.Usuarios
```

---

## 📝 Logs para Debugging

Los logs se muestran en la consola. Busca:
- `GET Request: ...` - Request enviado
- `GET Success: ...` - Request exitoso
- `HTTP Error: ...` - Error de conexión
- `JSON Deserialization Error: ...` - Problema de mapeo

---

## ✔️ Checklist de Verificación

- [ ] IP del PC: `172.18.92.169` (ejecuta `ipconfig`)
- [ ] API corriendo: `dotnet run` en `CineTEC.API`
- [ ] LocalDB corriendo (SQL Server)
- [ ] Datos en tabla `Usuarios`
- [ ] Firewall permite puerto 5000
- [ ] Emulador tiene acceso a red
- [ ] `ApiService.cs` tiene IP correcta
- [ ] `LoginPage.xaml.cs` inyecta servicios correctamente

---

## 🔗 URLs Útiles

| Descripción | URL |
|---|---|
| API Swagger | `http://172.18.92.169:5000/swagger` |
| Get Usuario por Cédula | `GET http://172.18.92.169:5000/api/usuarios/cedula/{cedula}` |
| Login | `POST http://172.18.92.169:5000/api/usuarios/login` |
| Listar Sucursales | `GET http://172.18.92.169:5000/api/Sucursales` |

---

## 📞 Próximos Pasos

Una vez que el login funcione:
1. [ ] Actualizar `CinesPage` para usar `CineService`
2. [ ] Crear servicio `PeliculasService`
3. [ ] Crear servicio `ProyeccionesService`
4. [ ] Crear servicio `AsientosService`
5. [ ] Agregar autenticación/token JWT
6. [ ] Implementar cierre de sesión

---
