# 🛫 Módulo Airport Admin (Administrador de Aeropuerto)

## 🎯 Objetivo
Organizar las operaciones de presentación, vistas e interacción de red para el administrador de un aeropuerto específico.

## 🧑‍💻 Usuarios
- Administrador de un Aeropuerto: Rol restringido a administrar entidades asociadas a **su aeropuerto**.
- **Regla Importante (UI)**: Este usuario **NUNCA** debe ver o seleccionar a qué aeropuerto pertenece, sus transacciones (llamadas API) usarán su Claims / Token provisto. Solo visualizará y creará elementos del aeropuerto asignado.

## 📌 Requerimientos del Módulo

### Vistas Necesarias (WPF)
1. **Dashboard Local / Monitor de Vuelos**:
   - Vuelos entrantes y salientes inmediatos (Próximas 24/48 horas).
   - Ocupación, retrasos o alertas en tiempo real.
2. **Gestión de Puertas de Embarque (Gates)**:
   - CRUD básico de puetas de embarque.
   - Estado de Gate (Ocupado, Mantenimiento, Libre).
3. **Gestión de Operadores / Personal Local**:
   - Creación de usuarios con rol "Operator".
   - Asignar operadores a roles de Mostrador (Check-in) o Embarque (Boarding).
4. **Programación / Gestión de Vuelos**:
   - Asignación de Gates, Aerolíneas y Horarios de Salida/Llegadas para el Aeropuerto.

### Componentes y ViewModels
- `AirportAdminDashboardViewModel`: Llama a un servicio `IFlightService` que trae estadísticas locales.
- `GateManagementViewModel`
- `FlightManagementViewModel`: Requiere Autocomplete o Dropdowns asíncronos para seleccionar Aerolíneas o Aviones desde la API.
- `OperatorManagementViewModel`: Para creación de operadores.

### UI Requisitos
- Vistas tabulares y de detalle.
- Filtros rápidos y paginación en grids WPF para no colapsar la memoria local frente a miles de vuelos históricos.