# 🎨 UX/UI, Heurísticas y Design Thinking (WPF)

## 🎯 Objetivo
Alinear el desarrollo de la aplicación WPF (`AeroVeloz.Desktop`) con los principios de diseño centrado en el usuario, garantizando usabilidad, estética y consistencia mediante las 10 heurísticas de Jakob Nielsen y metodologías de Design Thinking.

## 📐 Las 10 Heurísticas de Nielsen Aplicadas a AeroVeloz.Desktop

1. **Visibilidad del estado del sistema**:
   - `WPF`: Uso de `ProgressBar` o `RingSpinners` durante llamadas a la API. Mensajes en `StatusBar` (Cargando datos...).
   
2. **Relación entre sistema y mundo real**:
   - `WPF`: Textos claros en botones ("Asignar Puerta", "Boarding") en lugar de jerga de base de datos ("Insertar Registro en Tabla Turno"). Uso de Iconografía (aviones, maletas, pasaportes).

3. **Control y libertad del usuario**:
   - `WPF`: Implementar opción de "Deshacer" en caso de errores en formularios largos. Botones claros de "Cancelar" o cerrar modales (`DialogHost`).

4. **Consistencia y estándares**:
   - `WPF`: Usar librerías de componentes (ej: **MaterialDesignInXaml** o **MahApps.Metro**) para mantener la misma paleta de colores, márgenes, tipografías y botones en todas las vistas de Super Admin, Airport Admin y Operador.

5. **Prevención de errores**:
   - `WPF`: Deshabilitar botones (binding a `CanExecute` en el `RelayCommand`) si el formulario (ViewModel) tiene campos vacíos o correos inválidos. Diálogos de confirmación antes de eliminar o realizar operaciones críticas ("¿Seguro que desea eliminar el aeropuerto X?").

6. **Reconocer en lugar de recordar**:
   - `WPF`: Autocompletado de búsquedas (ComboBox virtuales filtrados por la API). Mantener a la vista filtros aplicados. Información residual útil en pantalla (Dashboard).

7. **Flexibilidad y eficiencia de uso**:
   - `WPF`: **Atajos de Teclado (KeyBindings)** esenciales para el Módulo Operador (ej. `Enter` o `Espacio` para confirmar check-in). Aceleradores rápidos.

8. **Estética y diseño minimalista**:
   - `WPF`: No saturar la pantalla con 100 botones. Mostrar solo la información estrictamente necesaria. (Balance y Simetría visual: Grids uniformes, spacing adecuado - `Margin="16"` estándar).

9. **Ayudar a los usuarios a reconocer y diagnosticar errores**:
   - `WPF`: Si falla una petición (e.g. `400 Bad Request`), mostrar el mensaje de la API ("El pasaporte ya fue registrado") con color **Rojo (Error)** y contexto, en vez de un "Error del servidor X099".

10. **Ayuda y documentación**:
    - `WPF`: Tooltips (`ToolTip="Asigna al pasajero un asiento"`) sobre los controles.

---

## 🚀 Conceptos Adicionales de Diseño UI

### Balance, Simetría, Regularidad y Previsibilidad:
- **Balance / Simetría**: Distribuir los controles equitativamente (uso de `Grid` y `Star Layouts` en XAML).
- **Regularidad**: Todos los botones principales abajo a la derecha, formularios centrados o a izquierda.
- **Previsibilidad**: El usuario debe prever qué pasará al dar click en "Siguiente".

### Design Thinking (Métricas Básicas a Considerar al Crear UI):
1. **Empatizar**: Interfaz del Operador (Módulo 3) enfocada en velocidad y luz brillante.
2. **Idear/Prototipar**: Bosquejos rápidos (Wireframes) trasladados a Vistas .xaml de manera modular (`UserControl`).

## 🛠 Herramientas Recomendadas para WPF
- **MaterialDesignThemes (WPF)**: Implementación de Material Design, animaciones suaves, modales integrados (`DialogHost`), `Snackbar` para notificaciones asíncronas de la API.
- Lógica de Notificaciones Push / Messenger (Patrón Pub/Sub).