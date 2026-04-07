# 🧑‍💻 Módulo Operador (Operador de Check-In)

## 🎯 Objetivo
Organizar las operaciones de presentación para el usuario Operador, optimizando rapidez, usabilidad y eficiencia.

## 🧑‍💻 Usuarios
- El usuario operador está registrado localmente en un Aeropuerto y asignado a una Aerolínea / Gate específico durante su turno.

## 📌 Requerimientos del Módulo

### Vistas Necesarias (User Controls de Vía Rápida)
1. **Asignación de Turno Inicio**:
   - Selector (Dropdown asíncrono) para el vuelo/gate al que se va a asignar.
2. **Proceso de Check-In**:
   - Búsqueda / Escaneo por Pasaporte, PNR (Código de Reserva), o Nombre.
   - Detalle rápido del Pasajero: Info, Estado del Vuelo, Equipaje.
   - Botón Gigante (Asignar Asiento y Confirmar Check-In).
   - Generación de Tarjeta de Embarque (PDF u otro, según requerimiento de API).
3. **Proceso de Abordaje (Boarding)**:
   - Pantalla sencilla: "Escaneo de Pase de Abordar" (Focus en TextBox permanente).
   - Indicador Visual (Verde = Abordado, Rojo = Error/No Registrado).
   - Contador de abordados vs Total del vuelo.

### Componentes y ViewModels
- `OperatorAppViewModel`: Orquesta las vistas de Check-In o Abordaje.
- `CheckInViewModel`: Usa `IPassengerService` y `IFlightService` para búsqueda rápida. Debounce binding (CommunityToolkit.Mvvm) para búsquedas.
- `BoardingViewModel`: Lógica de teclado rápido, notificaciones push asíncronas de abordajes exitosos.

### Restricciones UI Cero Complejidad (Nielsen)
- Pantallas sencillas: No requieren configuraciones elaboradas sino *Eficiencia del Usuario Experto*.
- Feedback inmediato.
- **Botones y Textos Grandes**, optimizados para atención rápida en mostrador o en puerta (Boarding).