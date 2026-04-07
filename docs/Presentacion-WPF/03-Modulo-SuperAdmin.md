# 👑 Módulo Super Admin

## 🎯 Objetivo
Organizar la estructura, restricciones y necesidades del Super Admin en la capa de UI.

## 🧑‍💻 Usuarios
- El Super Admin es global, su responsabilidad principal es la configuración general, creación de aeropuertos y creación de Administradores de Aeropuerto.

## 📌 Requerimientos del Módulo

### Vistas Necesarias (Views o UserControls)
1. **Dashboard Principal**:
   - Estadísticas globales (Cantidad de Aeropuertos, Administradores Totales, etc.).
   - Consumo de un endpoint `GET /api/stats/global` (por ejemplo).
2. **Gestión de Aeropuertos**:
   - CRUD de Aeropuertos. Vista Listado (`DataGrid`), Modal / Vista Detalle para Crear/Editar.
3. **Gestión de Administradores de Aeropuerto**:
   - Asignar usuarios al rol de "Airport Admin" y asignarlos a un Aeropuerto específico.

### Componentes y ViewModels
- `SuperAdminDashboardViewModel`: Orquesta estadísticas.
- `AirportListViewModel`, `AirportDetailViewModel`: Orquestan la lógica de creación de aeropuertos, comunicándose con un servicio `IAirportService`.
- `AdminListViewModel`: Servicio para crear administradores, usando el endpoint de Identity de la API y manejando roles.

### Restricciones UI
- Validación simple Frontend (e.g., *Nombre de Aeropuerto Requerido*).
- Ocultar opciones de módulos internos de un aeropuerto (Gate, Vuelos, Empleados) que no son responsabilidad directa (a menos que tenga un modo *Impersonar*).
- Validar respuestas de API al crear: si falla un requerimiento en el aeropuerto (ej. Nombre duplicado), mostrar el error retornado por la API (`400 Bad Request` + Detalles).