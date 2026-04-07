# 🔌 Consumo de APIs y Servicios: Buenas Prácticas y Estrategias (WPF)

## 🎯 Objetivo
Describir cómo se deben consumir los endpoints RESTful de la capa de backend en el cliente WPF de manera eficiente al garantizar bajo acoplamiento, escalabilidad, y siguiendo el principio DRY.

## 🔗 Estrategia de Conexión: `HttpClientFactory`
- **❌ Mala práctica:** Instanciar `new HttpClient()` manualmente provocando socket exhaustion.
- **✅ Buena práctica:** Usar `IHttpClientFactory` y registrar en inyección de dependencias (DI).

```csharp
builder.Services.AddHttpClient("AeroVelozApi", client =>
{
    client.BaseAddress = new Uri(Configuration["ApiSettings:BaseUrl"]);
    // Posible token interceptor para Jwt.
});
```

---

## 🚫 Principio DRY (Don't Repeat Yourself) en URLs
Para evitar hardcodear URLs:
1. Usar un archivo de configuración (e.g., `appsettings.json`) que albergue el **BaseUrl**.
2. Los servicios definen solo el sufijo de su endpoint.

```csharp
// ISomeService.cs
public interface IUserService {
    Task<IEnumerable<UserDto>> GetUsersAsync();
}

// UserService.cs (Impl)
public async Task<IEnumerable<UserDto>> GetUsersAsync()
{
    var httpClient = _httpClientFactory.CreateClient("AeroVelozApi");
    return await httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/v1/users");
}
```

---

## ✅ Manejo de Errores y Notificaciones Push
- **Respuestas de HTTP y Validaciones**: Procesar adecuadamente los códigos, como `400 Bad Request` para errores de validación, extrayendo los mensajes del servidor para ser mostrados en la UI.
- **Servicio de Diálogo y Snackbar**:
  Implementar una interfaz genérica `IDialogService` para que los ViewModels llamen métodos genéricos:
  ```csharp
  await _dialogService.ShowErrorAsync("El usuario ingresado ya existe en la base de datos.");
  ```
- **Fallo de Conexión o Timeout**: Capturar el error global (e.g. `HttpRequestException`) en el HttpClient para mostrar alertas de "Sin Conexión" sin que se crashee la App.
- **Lógica de Autenticación**: Definir un Handler o clase base para inyectar automáticamente el Bearer Token a las cabeceras antes de cada petición al API.

## ✅ Modelos de Datos (DTOs)
El cliente WPF solo manipulará **DTOs** para la serialización con System.Text.Json o Newtonsoft. No usará entidades del dominio.