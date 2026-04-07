# 🏗️ Arquitectura Base - Capa de Presentación (WPF)

## 🎯 Objetivo
Definir la estructura base de la aplicación de escritorio WPF, asegurando la separación de responsabilidades a través del patrón **MVVM (Model-View-ViewModel)** y la inyección de dependencias, acorde a las características de .NET 9.

## 📂 Estructura de Carpetas Recomendada en `AeroVeloz.Desktop`

Para mantener la escalabilidad y mantenibilidad, el proyecto debe estructurarse de la siguiente manera:

```text
AeroVeloz.Desktop/
│
├── Models/              # DTOs y modelos locales utilizados en la UI.
├── Views/               # Ventanas (Windows) y Controles de Usuario (UserControls) en XAML.
│   ├── SuperAdmin/
│   ├── AirportAdmin/
│   ├── Operator/
│   └── Shared/          # Componentes reutilizables, modales y layouts.
│
├── ViewModels/          # Lógica de presentación y orquestación de servicios (sin lógica de negocio).
│   ├── SuperAdmin/
│   ├── AirportAdmin/
│   ├── Operator/
│   └── Shared/
│
├── Services/            # Lógica de consumo de APIs y servicios locales.
│   ├── Http/            # Implementaciones de acceso a endpoints REST.
│   ├── Navigation/      # Servicio para navegar entre ViewModels/Views.
│   └── Dialog/          # Servicio para mostrar notificaciones, alertas y popups.
│
├── Utilities/           # Helpers, Converters (IValueConverter) genéricos.
├── Messages/            # Eventos de comunicación desacoplada entre ViewModels (p. ej. Messenger de CommunityToolkit).
└── App.xaml             # Inicio de la aplicación y configuración de DI (Dependency Injection).
```

## 🧩 Patrón MVVM y Principios
1. **Views**: Exclusivamente XAML y code-behind mínimo (solo lo obligatorio para la UI). No hay lógica ni consumo de APIs aquí.
2. **ViewModels**: Implementan `INotifyPropertyChanged` (recomendado usar `CommunityToolkit.Mvvm`).
3. **Models**: Clases simples (POCOs) para representar los datos.

## 💉 Inyección de Dependencias
Aprovechar el Generic Host de .NET (`Microsoft.Extensions.Hosting`) en WPF para inyectar servicios, `IHttpClientFactory` y ViewModels.

```csharp
// Ejemplo en App.xaml.cs
builder.Services.AddSingleton<MainViewModel>();
builder.Services.AddTransient<MainWindow>();
```