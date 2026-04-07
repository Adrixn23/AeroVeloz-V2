# 🧩 Integración de Capa de Presentación con APIs

## 🎯 Objetivo
Implementar la integración entre la capa de presentación y las APIs desarrolladas en la capa de aplicación, garantizando un consumo eficiente, desacoplado y mantenible de los servicios backend, bajo principios de arquitectura limpia y diseño orientado a servicios.

---

## 📌 Contexto
Esta práctica forma parte del proyecto final del curso y tiene como propósito consolidar la implementación de arquitecturas modernas basadas en APIs.

Los estudiantes deberán consumir las APIs previamente desarrolladas (RESTful en ASP.NET Core o similar) desde la interfaz de usuario (ASP.NET MVC, Razor, React u otra), aplicando buenas prácticas como:

- Separación de responsabilidades (Presentation vs API)
- Uso de DTOs para comunicación
- Manejo de errores y estados HTTP
- Validaciones distribuidas (cliente y servidor)
- Diseño estructurado de la capa de presentación

---

## 🛠️ Actividades

### 1. 🔌 Diseño de la integración con APIs

#### 📍 Identificación de endpoints
- `GET` → Consultas
- `POST` → Creación
- `PUT / PATCH` → Actualización
- `DELETE` → Eliminación

#### 📦 Modelos de consumo
- Definir DTOs de entrada y salida

#### 🔗 Comunicación
- Backend (.NET): `HttpClient` / `HttpClientFactory`
- Frontend (JS): `fetch` / `axios`

#### 📑 Contratos
- Formato estándar: JSON
- Manejo de códigos HTTP:
  - `200 OK`
  - `400 Bad Request`
  - `404 Not Found`
  - `500 Internal Server Error`

---

### 2. 🏗️ Arquitectura lógica de la capa de presentación

#### 📦 Componentes mínimos esperados

##### 🎮 Controladores / Pages / Components
- Orquestan la interacción del usuario

##### 🔄 Servicios de consumo de API
- Encapsulan llamadas HTTP  
- ❗ No realizar llamadas directas desde controladores

##### 📊 ViewModels / Models
- Representan los datos que se muestran en la UI

##### 🧰 Helpers / Utilities (opcional)
- Manejo de formatos, validaciones, etc.

---

#### 📉 Diagrama requerido
Debe representar:
- Flujo entre UI, servicios y API
- Separación de capas

---

#### ✅ Buenas prácticas a evaluar
- Separación clara de responsabilidades
- Bajo acoplamiento
- Reutilización de servicios
- No mezclar lógica de negocio en la UI

---

### 3. ⚙️ Implementación técnica

#### 🔌 Consumo de APIs
- Implementar servicios en la capa de presentación

#### 🔄 Flujo de interacción
1. Usuario interactúa con la UI
2. Controlador / Componente invoca servicio API
3. API procesa lógica de negocio
4. Retorna respuesta (JSON)
5. UI renderiza resultados

---

#### 🖥️ Integración con la UI

##### ASP.NET MVC
- Controladores consumen servicios API
- Uso de ViewModels

##### React / Frontend
- Hooks (`useEffect`)
- Manejo de estado (`React Query` / `Redux`)

---

#### ✔️ Validaciones

##### Frontend
- Required fields
- Validaciones de formularios

##### Backend
- Validaciones en API:
  - FluentValidation
  - DataAnnotations

---

#### 🎨 Renderizado
- ASP.NET MVC:
  - Partial Views
- React:
  - Componentes reutilizables

---

### 4. 🧪 Pruebas funcionales

#### 🔍 Validaciones
- Consumo correcto de endpoints
- Manejo de errores (API caída, timeout, etc.)
- Flujo completo UI → API → UI

#### 🧩 Casos de prueba
- Datos válidos
- Datos inválidos
- API no disponible

---

## 📦 Entregables

### 💻 Código fuente
- UI integrada con consumo de APIs
- Servicios desacoplados correctamente implementados

---

### 🎥 Evidencia de funcionamiento
- Video o capturas mostrando:
  - CRUD vía API
  - Validaciones
  - Manejo de errores

---

### 📄 Documento técnico (máx. 3 páginas)

Debe incluir:

1. Arquitectura de la solución  
2. Arquitectura lógica de la capa de presentación (**OBLIGATORIO**)  
   - Diagrama  
   - Explicación de componentes  
3. Diseño de integración con APIs  
4. Estrategia de consumo  
5. Conclusiones  

---