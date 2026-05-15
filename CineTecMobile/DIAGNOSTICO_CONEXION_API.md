## GUÍA DE DIAGNOSTICO - Conexión App Móvil ↔ API

### Cambios Realizados:

1. **CinesPage.xaml.cs**: Ahora usa `CineService` inyectado en lugar de crear su propio `HttpClient`
2. **CineService.cs**: Usa endpoint `/admin/Sucursales` 
3. **ApiService.cs**: BaseUrl = `http://172.18.92.169:5000/api` (IP de la PC)
4. **MauiProgram.cs**: Registra `CinesPage` con inyección de dependencias

---



## CAMBIO RÁPIDO DE IP:

Si necesitas cambiar la IP del API:

1. Abre: `Services/ApiService.cs`
2. Línea 21: `private const string BaseApiUrl = "http://172.18.122.238:5000/api";`
3. Reemplaza `172.18.122.238` con tu IP
4. Reconstruye y ejecuta: Ctrl+Shift+B, luego F5





## PASOS DE PRUEBA:

### 1. Verificar que el servidor API esté ejecutándose

```
URL: http://172.18.92.169:5000
Verifica que tu servidor esté en esa IP y puerto 5000
```

**Si tu PC tiene OTRA IP:**
- Abre PowerShell y ejecuta: `ipconfig | Select-String "IPv4"`
- Ve a `Services/ApiService.cs` línea 21
- Cambia: `private const string BaseApiUrl = "http://172.18.122.238:5000/api";`
- Reemplaza `172.18.122.238` con tu IP

---

### 2. Probar el endpoint en Postman o navegador

```
GET: http://172.18.92.169:5000/api/admin/Sucursales
```

**Respuesta esperada:**
```json
[
  {
	"id": 1,
	"nombre": "CineTEC Matriz",
	"ubicacion": "San José",
	"nombreCompleto": "CineTEC Matriz - San José"
  },
  ...
]
```

**Si devuelve error:**
- Verifica que la base de datos tenga datos
- Revisa los logs del API para errores
- Verifica permisos de acceso al endpoint `/admin/Sucursales`

---

### 3. Revisar logs en Visual Studio

Cuando ejecutas la app en Debug:
- Ve a: **Debug → Windows → Output** (o presiona Ctrl+Alt+O)
- Selecciona: **Debug** en el dropdown
- Busca logs que comiencen con:
  - `[ApiService]` - Solicitudes HTTP
  - `[CineService]` - Obtención de cines
  - `[CinesPage]` - Errores en la página

**Ejemplo de logs esperados:**
```
[ApiService] Inicializado con BaseUrl: http://192.168.100.21:5000/api
[CineService] Obteniendo lista de cines desde el API
[ApiService] GET Request: http://192.168.100.21:5000/api/admin/Sucursales
[CineService] Se obtuvieron 3 cines correctamente
```

**Ejemplo de error:**
```
[ApiService] Error de conexión HTTP al obtener cines: The server did not respond
```

---

### 4. Verificar modelo de datos

Si obtienes error de deserialización, verifica que los campos en la API coincidan con el modelo:

**Modelo en app (Models/Cine.cs):**
```csharp
public class Cine
{
	public int Id { get; set; }
	public string Nombre { get; set; }
	public string Ubicacion { get; set; }
}
```

**Respuesta del API debe tener (como mínimo):**
```json
{
  "id": 1,
  "nombre": "...",
  "ubicacion": "..."
}
```

---

### 5. Errores comunes y soluciones:

| Error | Causa | Solución |
|-------|-------|----------|
| "No hay cines disponibles" | Lista vacía o falla en BD | Verifica datos en base de datos |
| "Error de conexión" | IP incorrecta o servidor offline | Actualiza IP en ApiService.cs |
| "Error al cargar cines: ..." | Error en deserialización JSON | Verifica nombres de campos |
| Timeout (30s) | Servidor muy lento | Aumenta timeout en ApiService.cs línea 26 |
| 404 Not Found | Endpoint incorrecto | Verifica `/admin/Sucursales` existe |
| 401/403 Forbidden | Sin permisos | Verifica autenticación/autorización |

---



## PRUEBA EXITOSA:

✅ Cuando cargas CinesPage, deberías ver:
- El ActivityIndicator desaparece
- El Picker se llena con los cines
- Aparece un dropdown seleccionable con los cines

