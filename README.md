# ⚔️ Valhalla — Viking Management System

Sistema de gestión de guerreros vikingos construido con arquitectura limpia en **.NET 8**, separando responsabilidades en múltiples capas y comunicando dos aplicaciones web a través de una API REST interna.

---

## Tabla de contenidos

- [Descripción general](#descripción-general)
- [Arquitectura de la solución](#arquitectura-de-la-solución)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Requisitos previos](#requisitos-previos)
- [Clonar el repositorio](#clonar-el-repositorio)
- [Configuración](#configuración)
- [Ejecutar el proyecto](#ejecutar-el-proyecto)
- [Estructura de carpetas](#estructura-de-carpetas)
- [Principios aplicados](#principios-aplicados)
- [Endpoints de la API](#endpoints-de-la-api)

---

## Descripción general

Valhalla permite registrar, consultar, editar y eliminar guerreros vikingos. La solución se compone de dos aplicaciones ejecutándose en paralelo:

- **Valhalla.Api** — API REST que expone los datos de los vikingos.
- **Valhalla.Mvc** — Aplicación web con interfaz visual que consume la API mediante `HttpClient` tipado.

Ambas aplicaciones comparten modelos y constantes a través de los proyectos `Valhalla.Shared`, `Valhalla.Domain` y `Valhalla.Application`, siguiendo los principios SOLID y Clean Architecture.

---

## Arquitectura de la solución

```
Valhalla (solución)
├── Valhalla.Api              → API REST (controladores, Program.cs, appsettings)
├── Valhalla.Mvc              → Aplicación MVC (vistas, servicios, Program.cs)
├── Valhalla.Application      → Interfaces de servicio (IVikingApiService)
├── Valhalla.Domain           → Entidades del dominio
├── Valhalla.Infrastructure   → Implementaciones de acceso a datos
└── Valhalla.Shared           → DTOs y constantes compartidas (ApiRoutes)
```

### Flujo de comunicación

```
Navegador → Valhalla.Mvc → VikingApiService (HttpClient tipado) → Valhalla.Api → Base de datos
```

### Decisiones de diseño clave

| Elemento | Ubicación | Razón |
|---|---|---|
| `ApiRoutes.cs` (constantes de rutas) | `Valhalla.Shared` | Compartido entre Api y Mvc; un solo lugar para cambiarlas |
| `VikingDto` | `Valhalla.Shared` | Modelo de transferencia usado por ambas aplicaciones |
| `IVikingApiService` | `Valhalla.Application` | Inversión de dependencias (SOLID) |
| `VikingApiService` | `Valhalla.Mvc/Services` | Implementación concreta con `HttpClient` inyectado |
| `VikingApiSettings` | `Valhalla.Shared/Configuration` | Clase POCO enlazada a `appsettings.json` vía `IOptions<T>` |
| URL base de la API | `appsettings.json` de cada proyecto | Configurable por ambiente sin recompilar |

---

## Tecnologías utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core MVC
- ASP.NET Core Web API
- `IHttpClientFactory` con cliente tipado
- `IOptions<T>` para configuración fuertemente tipada
- Razor Views con Tag Helpers
- Entity Framework Core (en `Valhalla.Infrastructure`) — base de datos en memoria (`InMemory`)
- Bootstrap 5 + tipografía Cinzel / Crimson Pro (interfaz)

---

## Requisitos previos

Antes de clonar y ejecutar el proyecto asegúrate de tener instalado:

| Herramienta | Versión mínima | Descarga |
|---|---|---|
| .NET SDK | 8.0 | https://dotnet.microsoft.com/download |
| Visual Studio | 2022 (17.8+) | https://visualstudio.microsoft.com |
| Git | cualquiera | https://git-scm.com |

> También puedes usar **VS Code** con la extensión `C# Dev Kit` en lugar de Visual Studio.

---

## Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/valhalla.git
cd valhalla
```

---

## Configuración

### 1. Base de datos — `Valhalla.Api`

> ⚠️ **La base de datos es en memoria (`InMemory`).** No requiere SQL Server ni ninguna instalación adicional. Los datos se pierden al detener la aplicación — esto es intencional para desarrollo y demostración.

No es necesario modificar ninguna cadena de conexión. La configuración ya está lista en `Program.cs` de `Valhalla.Api`.

### 2. URL del API — `Valhalla.Mvc`

Abre `Valhalla.Mvc/appsettings.json` y verifica que la URL apunta al puerto donde correrá `Valhalla.Api`:

```json
{
  "VikingApi": {
    "BaseUrl": "https://localhost:7041/"
  }
}
```

> El puerto `7041` es el valor por defecto en desarrollo. Si Visual Studio asigna otro puerto al Api, actualiza este valor.

### 3. Migraciones

> No se requieren migraciones. Al usar base de datos en memoria, EF Core crea el esquema automáticamente en cada arranque.

---

## Ejecutar el proyecto

El proyecto requiere que **ambas aplicaciones corran al mismo tiempo**.

### Opción A — Visual Studio (recomendado)

1. Abre `Valhalla.sln` en Visual Studio 2022.
2. En el Explorador de soluciones, haz clic derecho en la solución → **Establecer proyectos de inicio**.
3. Selecciona **Varios proyectos de inicio**.
4. Marca `Valhalla.Api` y `Valhalla.Mvc` ambos con la acción **Iniciar**.
5. Presiona **F5** o el botón ▶ **Iniciar**.

Visual Studio abrirá los dos proyectos en paralelo automáticamente.

### Opción B — Terminal (dos ventanas)

**Terminal 1 — API:**
```bash
cd Valhalla.Api
dotnet run
```

**Terminal 2 — MVC:**
```bash
cd Valhalla.Mvc
dotnet run
```

Luego abre tu navegador en la URL que muestre el proyecto MVC (por defecto `https://localhost:7XXX`).

---

## Estructura de carpetas

```
Valhalla.Api/
├── Controllers/
│   └── VikingsController.cs        ← Endpoints REST
├── appsettings.json                ← Conexión a base de datos
└── Program.cs

Valhalla.Mvc/
├── Controllers/
│   └── VikingsController.cs        ← Acciones MVC (Index, Create, Edit, Delete)
├── Services/
│   └── VikingApiService.cs         ← HttpClient tipado que consume la API
├── Configuration/
│   └── VikingApiSettings.cs        ← POCO enlazado a appsettings
├── Views/Vikings/
│   ├── Index.cshtml                ← Listado de vikingos
│   ├── Create.cshtml               ← Formulario de creación
│   └── Edit.cshtml                 ← Formulario de edición
├── appsettings.json                ← BaseUrl de la API
└── Program.cs                      ← Registro de servicios y HttpClient

Valhalla.Shared/
├── DTOs/
│   └── VikingDto.cs                ← Modelo de transferencia compartido
├── Constants/
│   └── ApiRoutes.cs                ← Rutas de la API como constantes
└── Configuration/
    └── VikingApiSettings.cs        ← Clase de configuración tipada

Valhalla.Application/
└── Interfaces/
    └── IVikingApiService.cs        ← Contrato del servicio

Valhalla.Domain/
└── Entities/
    └── Viking.cs                   ← Entidad de dominio

Valhalla.Infrastructure/
├── Data/
│   └── AppDbContext.cs
└── Migrations/
```

---

## Principios aplicados

**SOLID**

- **S** — Responsabilidad única: cada clase tiene una sola razón de cambiar (`VikingApiService` solo gestiona la comunicación HTTP).
- **O** — Abierto/cerrado: nuevos endpoints se agregan en `ApiRoutes` sin modificar el servicio.
- **L** — Sustitución de Liskov: `VikingApiService` implementa `IVikingApiService`; el controlador trabaja contra la interfaz.
- **I** — Segregación de interfaces: `IVikingApiService` expone solo lo necesario para la vista.
- **D** — Inversión de dependencias: `VikingsController` depende de `IVikingApiService`, no de la implementación concreta.

**Clean Architecture**

Las capas internas (`Domain`, `Application`) no conocen las externas (`Infrastructure`, `Mvc`). La dependencia siempre apunta hacia adentro.

**Configuración por ambiente**

La URL de la API y la cadena de conexión viven en `appsettings.json`. Para producción basta crear `appsettings.Production.json` con los valores reales sin tocar una sola línea de código.

---

## Endpoints de la API

Base URL: `https://localhost:7041/api/vikings`

| Método | Ruta | Descripción |
|---|---|---|
| `GET` | `/api/vikings` | Obtiene todos los vikingos |
| `GET` | `/api/vikings/{id}` | Obtiene un vikingo por ID |
| `POST` | `/api/vikings` | Crea un nuevo vikingo |
| `PUT` | `/api/vikings/{id}` | Actualiza un vikingo existente |
| `DELETE` | `/api/vikings/{id}` | Elimina un vikingo |

Las rutas están definidas como constantes en `Valhalla.Shared/Constants/ApiRoutes.cs` y son el único lugar donde deben modificarse.

---

> *"Los que mueren en batalla van a Valhalla. Los que escriben código limpio, también."*
